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

    private void Start()
    {
        //sets up the animation we want for the ghost
        anim = GetComponent<Animator>();
        freeCam = GameObject.FindGameObjectWithTag("MainCamera");
        switchingCharacters = GameObject.FindObjectOfType<SwitchingCharacters>();
    }

    // Update is called once per frame
    void LateUpdate()
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
                transform.position = new Vector2(transform.position.x + direction.x * movementSpeed * Time.deltaTime, transform.position.y + direction.y * movementSpeed * Time.deltaTime);
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
