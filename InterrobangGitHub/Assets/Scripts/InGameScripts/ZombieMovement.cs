﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ZombieMovement : MonoBehaviour
{
    Animator anim; //animation for the player

    //all exposed variables to the editor
    public GameObject ghost;
    public bool active = false;
    public bool onLadder = false;
    public Vector3 cameraOffset;
    [Range(0, 10)]
    public float moveSpeed = 5f;
    [Range(0, 5)]
    public float waitTimeForPlatformToCrumble = 1;
    public float timeToStopStompAnimation;
    [Range(0, 5)]
    public float waitTimeForDoorToTakeDamage = 1;
    [Range(0, 2)]
    public float animationPlaySpeed = 1;
    public bool ghostFollowZombie;
    public bool mainCharacter = false;

    float dirX, dirY;
    int numOfCrumblePlatforms;

    //zombie movement left and right vars
    private Vector3 mousePos;
    [HideInInspector]
    public Rigidbody2D rb;
    private Vector2 direction;
    [Range(1f, 10f)]

    Vector2 finalMovement;

    Vector2 zombiePos;

    //zombie stomp ability vars
    float stompCounter = 0;
    [Range(0, 10)]
    float stompAdder = 1;
    bool isZombieStomping = false;
    public Button stompButton;

    //PlatformCrumble platfromCrumble;
    GameObject platform;

    //melee ability vars
    public Button meleeButton;
    bool isZombieMelee = false;
    float meleeCounter = 0;
    float meleeAdder = 1;
    int numOfDoors;

    SwitchingCharacters switchingCharacters;

    private void Start()
    {
        //sets up the animation we want
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        switchingCharacters = GameObject.FindObjectOfType<SwitchingCharacters>();
    }

    // Update is called once per frame
    void Update()
    {
        //stops the ghost and the zombie from colliding with each other 
        Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), ghost.GetComponent<Collider2D>(), true);

        //when the zombie is being controlled 
        if (!mainCharacter)
        {
            if (active == true && switchingCharacters.charactersCanMove)
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
                    //changes to the ladder weight
                    anim.SetLayerWeight(1, 1);
                    anim.speed = Mathf.Abs(direction.y * (moveSpeed / 2)) + Mathf.Abs(direction.x * (moveSpeed / 2)) / 2;
                    //add in the y axis
                    transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y + direction.y * (moveSpeed / 2) * Time.deltaTime);
                    //no gravity for being on ladder
                    rb.gravityScale = 0;
                }
                else
                {
                    //when the zombie comes off of the ladder set 
                    //everything back to where it should be
                    rb.gravityScale = 1;
                    anim.SetLayerWeight(1, 0);
                    //changes the frame rate at which the zombie animations are played 
                    anim.speed = animationPlaySpeed;
                }

            }
            else
            {
                //stop the zombie from playing the wrong animation when switching characters 
                anim.SetFloat("speed", 0f);

                //stops the animation speed playing when the ghost is selected and the zombie is on a ladder
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("Climbing"))
                {
                    anim.speed = 0;
                }
            }
        }
        else
        {
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
                    //changes to the ladder weight
                    anim.SetLayerWeight(1, 1);
                    anim.speed = Mathf.Abs(direction.y * (moveSpeed / 2)) + Mathf.Abs(direction.x * (moveSpeed / 2)) / 2;
                    //add in the y axis
                    transform.position = new Vector2(transform.position.x + direction.x * moveSpeed * Time.deltaTime, transform.position.y + direction.y * (moveSpeed / 2) * Time.deltaTime);
                    //no gravity for being on ladder
                    rb.gravityScale = 0;
                }
                else
                {
                    //when the zombie comes off of the ladder set 
                    //everything back to where it should be
                    rb.gravityScale = 1;
                    anim.SetLayerWeight(1, 0);
                    //changes the frame rate at which the zombie animations are played 
                    anim.speed = animationPlaySpeed;
                }

            }
            else
            {
                //stop the zombie from playing the wrong animation when switching characters 
                anim.SetFloat("speed", 0f);

                //stops the animation speed playing when the ghost is selected and the zombie is on a ladder
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("Climbing"))
                {
                    anim.speed = 0;
                }
            }
        }

        if(ghostFollowZombie)
        {
            zombiePos = gameObject.transform.position;
            zombiePos.x = zombiePos.x - 1;

            ghost.transform.position = zombiePos;
        }
    }

    private void OnEnable()
    {
        //stompButton.onClick.AddListener(stompSwitch);
        //meleeButton.onClick.AddListener(meleeSwitch);
    }



    void movementLeftRightUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stomp") || anim.GetCurrentAnimatorStateInfo(0).IsName("StompLeft"))
        {
            //stops the zombie from being able to move during the stomp animations 
            Vector2 freezeXVelocity;
            freezeXVelocity.x = 0;
            freezeXVelocity.y = rb.velocity.y;
            rb.velocity = freezeXVelocity;
        }
        else
        {
            //sets where the screen is touched to the zombie movement 
            if (Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePos - transform.position).normalized;
            }
            else direction = Vector2.zero;

            //passes the zombie movement to the animator to play the right animation 
            anim.SetFloat("speed", direction.x);


            //set the velocity of the rigid body to the direction of the mouse
            finalMovement.x = direction.x * moveSpeed;
            finalMovement.y = rb.velocity.y;

            //if not on ladder then set the velocity 
            if (onLadder == false)
            {
                rb.velocity = finalMovement;
            }

            if (onLadder)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void stompUpdate()
    {
        //find all crumble platforms that exist in the game
        var platfromCrumble = GameObject.FindObjectsOfType<PlatformCrumble>();
        //sets the number to the size of the array 
        numOfCrumblePlatforms = platfromCrumble.Length;

        //when the zombie is stomping start the timer 
        if (isZombieStomping == true)
        {
            stompCounter += stompAdder * Time.deltaTime;
            anim.SetBool("Stomp", true);
        }

        //let the zombie animation play for a bit before making the platform crumble
        if (stompCounter >= waitTimeForPlatformToCrumble)
        {
            for (int i = 0; i < numOfCrumblePlatforms; i++)
            {
                if (platfromCrumble[i].platformCanBreak == true)
                {
                    platfromCrumble[i].playAnimation = true;
                }
            }
        }

        //find the right type of platform and if it has been tapped on then start the stomp
        var tapToInteractAssets = GameObject.FindObjectsOfType<TapToInteract>();
        for (int i = 0; i < tapToInteractAssets.Length; i++)
        {
            //checks to see if any of the platforms have been tapped on 
            if (tapToInteractAssets[i].GetComponent<TapToInteract>().platformTapped == true)
            {
                stompSwitch();
            }
        }

        //zombie has finished stomping
        if (stompCounter >= timeToStopStompAnimation)
        {
            //reset all vars back to their original state
            isZombieStomping = false;
            anim.SetBool("Stomp", false);
            stompCounter = 0;
            for (int i = 0; i < tapToInteractAssets.Length; i++)
            {
                tapToInteractAssets[i].GetComponent<TapToInteract>().platformTapped = false;
            }
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
        numOfDoors = meleeDoor.Length;

        if (isZombieMelee == true)
        {
            meleeCounter += meleeAdder * Time.deltaTime;
            anim.SetBool("Melee", true);
        }

        //find the right type of melee asset and if it has been tapped on then start the melee
        var tapToInteractAssets2 = GameObject.FindObjectsOfType<TapToInteract>();
        for (int i = 0; i < tapToInteractAssets2.Length; i++)
        {
            //checks to see if any of the melee assets have been tapped on by the player 
            if (tapToInteractAssets2[i].GetComponent<TapToInteract>().meleeTapped == true)
            {
                meleeSwitch();
            }
        }

        //waits time before running to let the animation play for a bit
        if (meleeCounter >= waitTimeForDoorToTakeDamage)
        {
            for (int i = 0; i < numOfDoors; i++)
            {
                if (meleeDoor[i].doorWillTakeDamage == true && isZombieMelee == true)
                {
                    if (meleeDoor[i].doorHealth > 0)
                    {
                        //as long as the zombie is at the door and the health is greater than 0
                        //take away health
                        meleeDoor[i].doorHealth -= 0.5f;
                    }
                }

            }

            for (int i = 0; i < tapToInteractAssets2.Length; i++)
            {
                //checks to see if any of the melee assets have been tapped on by the player 
                tapToInteractAssets2[i].GetComponent<TapToInteract>().meleeTapped = false;
            }

            //reset everything
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
