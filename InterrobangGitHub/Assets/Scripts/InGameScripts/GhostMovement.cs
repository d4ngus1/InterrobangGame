using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GhostMovement : MonoBehaviour
{
    //exposed vars to the editor
    public bool active = false;
    public Vector3 cameraOffset;
    [Range(0, 10)]
    public float movementSpeed = 0.05f;
    [Range(0, 20)]
    public float floaty = 5;
    [Range(0, 2)]
    public float animationSpeed = 1;
    [Range(0, 2)]
    public float possessionTime = 1;

    GameObject freeCam;

    Animator anim; //animation for the ghost

    float dirX, dirY;
    [Range(1f, 20f)]

    bool dontMove;
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private SwitchingCharacters switchingCharacters;
    float move;

    Vector2 floatyness;
    Vector4 floatOfGhost;

    private void Start()
    {
        //sets up the animation we want for the ghost
        anim = GetComponent<Animator>();
        freeCam = GameObject.FindGameObjectWithTag("MainCamera");
        switchingCharacters = GameObject.FindObjectOfType<SwitchingCharacters>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && switchingCharacters.charactersCanMove)
        {
            if (Input.GetMouseButton(0))
            {
                //store the variable to be used in the animator to see whether 
                //the ghost is moving to the left or to the right

                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                move = direction.x;
                //transform.position = new Vector2(transform.position.x + direction.x * movementSpeed * Time.deltaTime, transform.position.y + direction.y * movementSpeed * Time.deltaTime);

                //allows the ghost to have more of a floaty feel 
                rb.velocity = direction * movementSpeed;
                floatyness = direction * movementSpeed;

                if (floatyness.x > 0 && floatyness.y > 0)
                {
                    //floating top right
                    floatOfGhost = new Vector4(1, 0, 0, 0);
                }
                else if (floatyness.x > 0 && floatyness.y < 0)
                {
                    //floating bottom right
                    floatOfGhost = new Vector4(0, 1, 0, 0);
                }
                else if (floatyness.x < 0 && floatyness.y < 0)
                {
                    //floating bottom left
                    floatOfGhost = new Vector4(0, 0, 1, 0);
                }
                else if (floatyness.x < 0 && floatyness.y > 0)
                {
                    //floating top left
                    floatOfGhost = new Vector4(0, 0, 0, 1);
                }
            }
            else
            {
                move = 0;
                direction = Vector2.zero;

                //top right
                if (floatOfGhost.x > 0)
                {
                    if (floatyness.x > 0)
                    {
                        floatyness.x -= floaty * Time.deltaTime;
                    }

                    if (floatyness.y > 0)
                    {
                        floatyness.y -= floaty * Time.deltaTime;
                    }

                    if (floatyness.x < 0)
                    {
                        floatyness.x = 0;
                    }

                    if (floatyness.y < 0)
                    {
                        floatyness.y = 0;
                    }

                    if (floatyness.x == 0 && floatyness.y == 0)
                    {
                        floatOfGhost.x = 0;
                    }
                }

                //bottom right
                if (floatOfGhost.y > 0)
                {
                    if (floatyness.x > 0)
                    {
                        floatyness.x -= floaty * Time.deltaTime;
                    }

                    if (floatyness.y < 0)
                    {
                        floatyness.y += floaty * Time.deltaTime;
                    }

                    if (floatyness.x < 0)
                    {
                        floatyness.x = 0;
                    }

                    if (floatyness.y > 0)
                    {
                        floatyness.y = 0;
                    }

                    if (floatyness.x == 0 && floatyness.y == 0)
                    {
                        floatOfGhost.y = 0;
                    }
                }

                //bottom left
                if (floatOfGhost.z > 0)
                {
                    if (floatyness.x < 0)
                    {
                        floatyness.x += floaty * Time.deltaTime;
                    }

                    if (floatyness.y < 0)
                    {
                        floatyness.y += floaty * Time.deltaTime;
                    }

                    if (floatyness.x > 0)
                    {
                        floatyness.x = 0;
                    }

                    if (floatyness.y > 0)
                    {
                        floatyness.y = 0;
                    }

                    if (floatyness.x == 0 && floatyness.y == 0)
                    {
                        floatOfGhost.z = 0;
                    }
                }

                //top left
                if (floatOfGhost.w > 0)
                {
                    if (floatyness.x < 0)
                    {
                        floatyness.x += floaty * Time.deltaTime;
                    }

                    if (floatyness.y > 0)
                    {
                        floatyness.y -= floaty * Time.deltaTime;
                    }

                    if (floatyness.x > 0)
                    {
                        floatyness.x = 0;
                    }

                    if (floatyness.y < 0)
                    {
                        floatyness.y = 0;
                    }

                    if (floatyness.x == 0 && floatyness.y == 0)
                    {
                        floatOfGhost.w = 0;
                    }
                }



                rb.velocity = floatyness;
            }

            //sends the data from the player to the condition in the animation to allow it to change transitions 
            anim.SetFloat("gSpeed", move);

            
            // transform.eulerAngles = new Vector2(0, -360);

        }
        else
        {
            //stop the ghost from playing the wrong animation when switching 
            anim.SetFloat("gSpeed", 0f);
        }

        //changes the animation speed
        anim.speed = animationSpeed;
    }

}
