﻿using UnityEngine;
using System.Collections;

public class LeverElevator : MonoBehaviour
{
    CameraPan cameraPan;
    GameObject ghost;
    ZombieMovement zombie;
    Animator ghostAnim;
    Animator anim;
    float animTimer;
    float turnGhostOff = 1;
    public GameObject elevatorObject;
    public GameObject elevatorStopper;
    [Range(-10, 10)]
    public float maxHeight = 0f;
    float initialHeight;
    [Range(0, 5)]
    public float elevatorSpeed = 0.05f; 
    bool ghostIsInside = false;
    bool isLeverOn = false;
    int clickCounter = 0;
    bool runTimer = false;
    bool elevatorUpdate = false;
    Vector2 ghostPosWhenLeverOn;
    LeverRotation leverRotation;

    [HideInInspector]
    public bool pan;
    ParticleSystem particle;
    SwitchingCharacters characters;

    private void Start()
    {
        //set up the ghost game object
        ghost = GameObject.FindGameObjectWithTag("ghost");
        zombie = GameObject.FindObjectOfType<ZombieMovement>();
        ghostAnim = ghost.GetComponent<Animator>();

        //set up the animator for the lever state
        anim = gameObject.GetComponent<Animator>();

        //gets the particle system off of the lever and sets it to false
        particle = gameObject.GetComponent<ParticleSystem>();
        characters = GameObject.FindObjectOfType<SwitchingCharacters>();
        particle.enableEmission = false;


        //set up the max height of the elevator
        initialHeight = elevatorObject.transform.localPosition.y;

        cameraPan = gameObject.GetComponent<CameraPan>();
        gameObject.GetComponent<CameraPan>().enabled = false;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks to see if its the ghost beside the lever
        if (collision.tag == "ghost")
        {
            ghostIsInside = true;
            anim.SetBool("Interactable", true);
            
            gameObject.GetComponent<CameraPan>().enabled = true;
        }
    }

    //when the ghost leaves the collider set it back to false 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ghost")
        {
            ghostIsInside = false;
            
            anim.SetBool("Interactable", false);
            gameObject.GetComponent<CameraPan>().enabled = false;
        }
    }

    private void Update()
    {
        if (ghostIsInside == true)
        {
            //when the leaver is on stop the ghost from being controlled as it is now possessing the switch 
            if (isLeverOn == true)
            {
                ghost.transform.position = ghostPosWhenLeverOn;

                if (animTimer > ghost.GetComponent<GhostMovement>().possessionTime)
                {
                    particle.enableEmission = true;
                    ghost.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    //ghost.SetActive(false);
                    runTimer = false;
                    elevatorUpdate = true;
                    animTimer = 0;
                    anim.SetBool("leverState", true);
                    
                }
            }
            else
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = 10;
                //ghost.SetActive(true);
            }
        }

        if (isLeverOn == false)
        {
            ElevatorUpdateOff();
        }

       

        if (runTimer == true)
        {
            animTimer += 1 * Time.deltaTime;
        }

        if(elevatorUpdate == true)
        {
            ElevatorUpdateOn();
        }
    }

    private void OnMouseDown()
    {
        //as long as the ghost is inside the area then the lever can be activated
        if (ghostIsInside == true && characters.counter == 2)
        {
            ghost.GetComponent<Animator>().SetFloat("gSpeed", 0);
            clickCounter++;

            //reset the counter 
            if (clickCounter > 2) clickCounter = 1;

            //if the lever is off then rotate it and start the emission 
            if (isLeverOn == false)
            {
                //start the possess animation
                ghostAnim.SetBool("possess", true);
                runTimer = true;
                ghostPosWhenLeverOn = ghost.transform.position;
                isLeverOn = true;
            }

            //if the lever is pressed again it is off so rotate back and turn the emission off 
            if (clickCounter == 2 && characters.counter == 2)
            {
                ghostAnim.SetBool("possess", false);
                elevatorUpdate = false;
                particle.enableEmission = false;
                anim.SetBool("leverState", false);
                isLeverOn = false;
            }
        }

    }

    private void ElevatorUpdateOn()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        if (pan)
        {
            cameraPan.objectPan = elevatorObject;
            cameraPan.panToObject = true;
        }

        yield return new WaitForSeconds(cameraPan.timeBeforePanToObject);

        //moves the platform up 
        if (elevatorObject.transform.localPosition.y < elevatorStopper.transform.localPosition.y)
        {
            elevatorObject.transform.Translate(0, elevatorSpeed * Time.deltaTime, 0);
        }

        
    }

    private void ElevatorUpdateOff()
    {

        cameraPan.panToObject = false;
        pan = true;

        if (elevatorObject.transform.localPosition.y > initialHeight)
        {
            elevatorObject.transform.Translate(0, -elevatorSpeed * Time.deltaTime, 0);
        }        
    }
}
