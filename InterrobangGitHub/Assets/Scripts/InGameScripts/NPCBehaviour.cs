using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public float movementSpeed;
    public float idleTime;

    Animator anim;
    bool movingRight;
    Vector2 direction;
    float idleCounter;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        direction.x = 1;
        idleCounter = idleTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if moving right and it's less than the right point then move the body 
        if (movingRight)
        {
            anim.SetBool("Walking Right", true);
            anim.SetBool("Walking Left", false);
            transform.position = new Vector2(transform.position.x + direction.x * movementSpeed, transform.position.y + direction.y * movementSpeed);
        }
        //if moving left and its greater than the left point then move the body right
        if (!movingRight)
        {
            anim.SetBool("Walking Left", true);
            anim.SetBool("Walking Right", false);
            transform.position = new Vector2(transform.position.x - direction.x * movementSpeed, transform.position.y - direction.y * movementSpeed);
        }

        //when the body hits the right side
        if (gameObject.transform.position.x >= rightPoint.transform.position.x)
        {
            anim.SetBool("Idle Right", true);
            direction.x = 0;
            idleCounter -= 1 * Time.deltaTime;

            if (idleCounter <= 0)
            {
                anim.SetBool("Idle Right", false);
                idleCounter = idleTime;
                direction.x = 1;
                movingRight = false;
            }
        }

        //when the body hits the left side
        if (gameObject.transform.position.x <= leftPoint.transform.position.x)
        {
            anim.SetBool("Idle Left", true);
            direction.x = 0;
            idleCounter -= 1 * Time.deltaTime;

            if (idleCounter <= 0)
            {
                anim.SetBool("Idle Left", false);
                idleCounter = idleTime;
                direction.x = 1;
                movingRight = true;
            }
        }
    }

    
}


