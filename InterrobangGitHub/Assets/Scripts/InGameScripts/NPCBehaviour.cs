using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public float movementSpeed;

    Animator anim;
    bool movingRight;
    Vector2 direction;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        direction.x = 1;
    }

    // Update is called once per frame
    void Update()
    {

        //if moving right and it's less than the right point then move the body 
        if (movingRight)
        {
            anim.SetBool("Walking Left", false);
            transform.position = new Vector2(transform.position.x + direction.x * movementSpeed, transform.position.y + direction.y * movementSpeed);
        }
        //if moving left and its greater than the left point then move the body right
        if (!movingRight)
        {
            anim.SetBool("Walking Left", true);
            transform.position = new Vector2(transform.position.x - direction.x * movementSpeed, transform.position.y - direction.y * movementSpeed);
        }

        //changed the body from left to right 
        if (gameObject.transform.position.x >= rightPoint.transform.position.x)
        {
            movingRight = false;
        }
        if (gameObject.transform.position.x <= leftPoint.transform.position.x)
        {
            movingRight = true;
        }
    }
}
