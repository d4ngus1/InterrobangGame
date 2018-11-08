using UnityEngine;
using System.Collections;

public class CharacterThroughLeverPortal : MonoBehaviour {

    public GameObject portalReciever;
    GameObject ghost, zombie;
    public GameObject lever;
    bool portalInteractable = false;
    bool ghostAtPortal = false;
    Animator anim;

    private void Start()
    {
        ghost = GameObject.Find("Player Ghost");
        zombie = GameObject.Find("Player Zombie");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (lever.GetComponent<LeverPortal>().isLeverOn == true)
        {
            //starts the animation for the portal
            anim.SetBool("Open", true);
        }
        else anim.SetBool("Open", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lever.GetComponent<LeverPortal>().isLeverOn == true)
        {
            if (collision.gameObject.tag == "zombie")
            {
                ghostAtPortal = false;
            }
            if (collision.gameObject.tag == "ghost")
            {
                ghostAtPortal = true;
            }

            portalInteractable = true;
        }
        else portalInteractable = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the player is already in the trigger when the lever is turned back on 
        if (lever.GetComponent<LeverPortal>().isLeverOn == true)
        {
            if (collision.gameObject.tag == "zombie")
            {
                ghostAtPortal = false;
            }
            if (collision.gameObject.tag == "ghost")
            {
                ghostAtPortal = true;
            }

            portalInteractable = true;
        }
        else portalInteractable = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Open", false);
        portalInteractable = false;
    }

    private void OnMouseDown()
    {
        if (portalInteractable == true)
        {
            //checks to see whether or not its the zombie or the ghost going through the portal 
            if (ghostAtPortal == false)
            {
                //set the position of the player to the portal reciever but keep the same z axis for the player(stops the object dissapearing)
                zombie.transform.position = new Vector3(portalReciever.transform.position.x, portalReciever.transform.position.y, zombie.transform.position.z);
            }
            if (ghostAtPortal == true)
            {
                ghost.transform.position = new Vector3(portalReciever.transform.position.x - 2, portalReciever.transform.position.y, ghost.transform.position.z);
            }
        }
    }
}
