using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GhostMovement : MonoBehaviour
{
    //exposed vars to the editor
    public bool active = false;
    [Range(0, 0.1f)]
    public float movementSpeed = 0.05f;
    [Range(0, 2)]
    public float animationSpeed = 1;
    [Range(0,2)]
    public float possessionTime = 1;

    Animator anim; //animation for the ghost

    float dirX, dirY;
    [Range(1f, 20f)]
    
    
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    float move;

    private void Start()
    {
        //sets up the animation we want for the ghost
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButton(0))
            {
                //store the variable to be used in the animator to see whether 
                //the ghost is moving to the left or to the right
                move = direction.x;
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                transform.position = new Vector2(transform.position.x + direction.x * movementSpeed, transform.position.y + direction.y * movementSpeed);
            }
            else move = 0;

            //sends the data from the player to the condition in the animation to allow it to change transitions 
            anim.SetFloat("gSpeed", move);

            transform.eulerAngles = new Vector2(0, -360);
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
