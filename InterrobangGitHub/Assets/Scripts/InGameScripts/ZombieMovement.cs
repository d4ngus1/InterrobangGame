using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ZombieMovement : MonoBehaviour
{
    Animator anim; //animation for the player
    public GameObject ghost;

    float dirX, dirY;
    [Range(1f, 20f)]
    //public float moveSpeed = 5f;
    public bool active = false;

    //zombie movement left and right vars
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private Vector2 direction;
    [Range(1f, 10f)]
    public float moveSpeed = 5f;

    //zombie stomp ability vars
    public float stompCounter = 0;
    [Range(0, 10)]
    public float stompAdder = 1;
    bool isZombieStomping = false;
    public Button stompButton;

    //PlatformCrumble platfromCrumble;
    GameObject platform;
    [Range(0, 2)]
    public float platformCrumbleTimer = 1;
    public int numOfCrumblePlatforms = 2;

    //melee ability vars
    public Button meleeButton;
    bool isZombieMelee = false;
    float meleeCounter = 0;
    [Range(0, 10)]
    public float meleeAdder = 1;
    public int numOfDoors;

    //ladders
    public bool onLadder = false;

    private void Start()
    {
        //sets up the animation we want
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //stops the ghost and the zombie from colliding with each other 
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), ghost.GetComponent<Collider2D>(), true);

        //when the zombie is being controlled 
        if (active == true)
        {
            //moves the zombie left and right 
            movementLeftRightUpdate();

            //zombie stomp ability
            stompUpdate();

            //zombie melee ability 
            meleeUpdate();

            //ladders
            if (onLadder)
            {
                anim.SetLayerWeight(1, 1);
                anim.speed = Mathf.Abs(direction.y * moveSpeed);
                transform.position = new Vector2(transform.position.x, transform.position.y + direction.y * moveSpeed * Time.deltaTime);
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 1;
                anim.SetLayerWeight(1, 0);
                anim.speed = 1;
            }

        }
        else
        {
            //stop the zombie from playing the wrong animation when switching characters 
            anim.SetFloat("speed", 0f);
        }
    }

    private void OnEnable()
    {
        stompButton.onClick.AddListener(stompSwitch);
        meleeButton.onClick.AddListener(meleeSwitch);
    }

    void movementLeftRightUpdate()
    {
        //sets where the screen is touched to the zombie movement 
        if (Input.mousePosition.x < 1710 || Input.mousePosition.y > 290)
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePos - transform.position).normalized;
                transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y);
            }
            else direction = Vector2.zero;

            //passes the zombie movement to the animator to play the right animation 
            anim.SetFloat("speed", direction.x);
        }
    }

    void stompUpdate()
    {
        var platfromCrumble = GameObject.FindObjectsOfType<PlatformCrumble>();


        //when the zombie is stomping start the timer 
        if (isZombieStomping == true)
        {
            stompCounter += stompAdder * Time.deltaTime;
            anim.SetBool("Stomp", true);
        }

        if (stompCounter >= platformCrumbleTimer)
        {
            for (int i = 0; i < numOfCrumblePlatforms; i++)
            {
                if (platfromCrumble[i].platformCanBreak == true)
                {
                    platfromCrumble[i].playAnimation = true;
                }
            }
        }

        //zombie has finished stomping
        if (stompCounter >= 1)
        {
            isZombieStomping = false;
            anim.SetBool("Stomp", false);
            stompCounter = 0;
        }

    }

    void stompSwitch()
    {
        if (isZombieStomping == false)
        {
            isZombieStomping = true;
        }
    }

    void meleeUpdate()
    {
        var meleeDoor = GameObject.FindObjectsOfType<DoorHealth>();

        if (isZombieMelee == true)
        {
            meleeCounter += meleeAdder * Time.deltaTime;
            anim.SetBool("Melee", true);
        }



        if (meleeCounter >= 1)
        {
            for (int i = 0; i < numOfDoors; i++)
            {
                if (meleeDoor[i].doorWillTakeDamage == true && isZombieMelee == true)
                {
                    if (meleeDoor[i].doorHealth > 0)
                    {
                        meleeDoor[i].doorHealth -= 0.5f;
                    }
                }

            }

            isZombieMelee = false;
            anim.SetBool("Melee", false);
            meleeCounter = 0;
        }


    }

    void meleeSwitch()
    {
        if (isZombieMelee == false)
        {
            isZombieMelee = true;
        }
    }






}
