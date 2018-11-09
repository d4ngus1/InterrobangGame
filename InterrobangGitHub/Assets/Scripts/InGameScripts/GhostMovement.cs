using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GhostMovement : MonoBehaviour
{

    Animator anim; //animation for the ghost

    float dirX, dirY;
    [Range(1f, 20f)]
    public float moveSpeed = 5f;
    public bool active = false;
    [Range(0.00f, 0.2f)]
    public float movementSlowdown;
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    public Vector2 direction;
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
                move = direction.x;
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                transform.position = new Vector2(transform.position.x + direction.x * movementSlowdown, transform.position.y + direction.y * movementSlowdown);
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
    }
}
