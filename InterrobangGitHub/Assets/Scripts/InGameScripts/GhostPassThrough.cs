using UnityEngine;
using System.Collections;

public class GhostPassThrough : MonoBehaviour
{

    public GameObject platformToPassThrough;
    BoxCollider2D platformToPassBoxCollider, triggerBoxCollider;

    // Use this for initialization
    void Start()
    {
        //gets the collider on the trigger
        triggerBoxCollider = GetComponent<BoxCollider2D>();
        //get the collider on the platform that the ghost wants to pass through 
        platformToPassBoxCollider = platformToPassThrough.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //checks to make sure its the ghost that has collided with the trigger
        if (collision.gameObject.tag == "ghost")
        {
            //changes the state of the platform to allow the ghost to pass through 
            platformToPassBoxCollider.isTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //checks to make sure its the ghost that has collided with the trigger
        if (collision.gameObject.tag == "ghost")
        {
            //changes the platform back to being solid so that the zombie cant pass through anymore 
            platformToPassBoxCollider.isTrigger = false;
        }
    }
}
