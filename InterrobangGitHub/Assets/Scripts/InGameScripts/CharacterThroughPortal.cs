﻿using UnityEngine;
using System.Collections;

public class CharacterThroughPortal : MonoBehaviour
{

    public GameObject portalReciever;
    GameObject ghost, zombie;
    bool portalInteractable = false;
    bool ghostAtPortal = false;
    Animator anim;

    private void Start()
    {
        ghost = GameObject.Find("Player Ghost");
        zombie = GameObject.Find("Player Zombie");
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            //starts the animation for the portal
            anim.SetBool("Open", true);

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
