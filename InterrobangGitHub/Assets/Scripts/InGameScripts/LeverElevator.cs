﻿using UnityEngine;
using System.Collections;

public class LeverElevator : MonoBehaviour
{
    GameObject ghost;
    public GameObject highlight;
    public GameObject elevatorObject;
    [Range(0, 10)]
    public float maxHeight = 0f;
    float initialHeight;
    [Range(0, 1)]
    public float elevatorSpeed = 0.05f; 
    bool ghostIsInside = false;
    bool isLeverOn = false;
    int clickCounter = 0;


    ParticleSystem particle;
    SwitchingCharacters characters;

    private void Start()
    {
        //set up the ghost game object
        ghost = GameObject.FindGameObjectWithTag("ghost");

        //gets the particle system off of the lever and sets it to false
        particle = gameObject.GetComponent<ParticleSystem>();
        characters = GameObject.FindObjectOfType<SwitchingCharacters>();
        particle.enableEmission = false;
        highlight.SetActive(false);

        //set up the max height of the elevator
        initialHeight = elevatorObject.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks to see if its the ghost beside the lever
        if (collision.tag == "ghost")
        {
            ghostIsInside = true;
            highlight.SetActive(true);
        }


    }

    //when the ghost leaves the collider set it back to false 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ghost")
        {
            ghostIsInside = false;
            //allows the player to know when it can be interacted with 
            highlight.SetActive(false);
        }


    }

    private void Update()
    {
        if (ghostIsInside == true)
        {
            //when the leaver is on stop the ghost from being controlled as it is now possessing the switch 
            if (isLeverOn == true)
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = -5;
                ghost.SetActive(false);
                ElevatorUpdateOn();
            }
            else
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = 1;
                ghost.SetActive(true);
            }
        }

        if (isLeverOn == false)
        {
            ElevatorUpdateOff();
        }

        //keeps the highlight behind the lever
        highlight.gameObject.transform.rotation = gameObject.transform.rotation;
    }

    private void OnMouseDown()
    {
        //as long as the ghost is inside the area then the lever can be activated
        if (ghostIsInside == true && characters.counter == 2)
        {
            clickCounter++;

            //reset the counter 
            if (clickCounter > 2) clickCounter = 1;

            //if the lever is off then rotate it and start the emission 
            if (isLeverOn == false)
            {
                transform.Rotate(new Vector3(0, 0, 100));

                particle.enableEmission = true;
                isLeverOn = true;
            }

            //if the lever is pressed again it is off so rotate back and turn the emission off 
            if (clickCounter == 2 && characters.counter == 2)
            {
                transform.Rotate(new Vector3(0, 0, -100));

                particle.enableEmission = false;

                isLeverOn = false;
            }
        }

    }

    private void ElevatorUpdateOn()
    {
        //moves the platform up 
        if (elevatorObject.transform.position.y < maxHeight)
        {
            elevatorObject.transform.Translate(0, elevatorSpeed, 0);
        }
    }

    private void ElevatorUpdateOff()
    {
        if (elevatorObject.transform.position.y > initialHeight)
        {
            elevatorObject.transform.Translate(0, -elevatorSpeed, 0);
        }
    }
}
