using UnityEngine;
using System.Collections;
using System;

public class ZombieHeadThrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject zombieHead, zombie;
    private Vector3 startPos, endPos;
    private bool releaseHead, returnHead, stopZombieMoving, startUpdate;
    private float lerpTime = 1f, currentLerpTime, lerpPerc;

    public bool zombieIsInside;
    public float headThrowSpeed;

    void Start()
    {
        //sets up the zombie and the head 
        zombieHead = GameObject.FindGameObjectWithTag("zombieHead");
        zombie = GameObject.FindGameObjectWithTag("zombie");
        lineRenderer = GameObject.FindGameObjectWithTag("zombieHead").GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (startUpdate)
        {
            //keeps the zombie head in the same position as the zombie body
            zombieHead.transform.position = zombie.transform.position;

            //switches the head from going to the target and returning back to the zombie
            ZombieHeadMovementDirection();

            //update the position of the line render
            lineRenderer.SetPosition(0, zombie.transform.position);
            lineRenderer.SetPosition(1, zombieHead.transform.position);

            //stop the zombie from moving 
            if (stopZombieMoving == true)
            {
                zombie.transform.position = startPos;
                //set the zombie animation to headless
                zombie.GetComponent<Animator>().SetLayerWeight(2, 1);
            }

            //update the lerp values for the vector lerp
            LerpUpdate();
            Debug.Log(zombieHead.transform.position);
        }
    }
    private void ZombieHeadMovementDirection()
    {
        //when true starts moving the head to the target 
        if (releaseHead == true)
        {
            returnHead = false;
            HeadMovment(startPos, endPos);
        }

        //when the head has reached the target bounce it back the other way 
        if (zombieHead.transform.position == gameObject.transform.position)
        {
            releaseHead = false;
            returnHead = true;
            currentLerpTime = 0;
            //what the target does when the zombie head has hit it
            WhenHeadHits();
        }

        //start the lerp the other way round
        if (returnHead == true)
        {
            HeadMovment(endPos, startPos);
        }
    }

    private void WhenHeadHits()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void HeadMovment(Vector3 startPos, Vector3 endPos)
    {
        //percentage
        lerpPerc = currentLerpTime / lerpTime;
        //smoothed movement
        zombieHead.transform.position = Vector3.Lerp(startPos, endPos, lerpPerc);
    }
    private void OnMouseDown()
    {
        if (zombieIsInside)
        {
            startUpdate = true;
            //sets the vectors to be passed into the lerp function
            startPos = zombie.transform.position;
            endPos = gameObject.transform.position;
            currentLerpTime = 0;
            releaseHead = true;
            //makes the zombie head visible to the player 
            zombieHead.GetComponent<SpriteRenderer>().enabled = true;
            lineRenderer.enabled = true;
            //stops the zombie from changing position whilst the head throw is being used
            stopZombieMoving = true;
        }
    }
    private void LerpUpdate()
    {
        //increment the value 
        currentLerpTime += Time.deltaTime * headThrowSpeed;

        //when it goes more keep it there
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;

            //make the zombie head invisible again 
            if (returnHead == true)
            {
                zombieHead.GetComponent<SpriteRenderer>().enabled = false;
                lineRenderer.enabled = false;
                stopZombieMoving = false;
                //set the zombie animation back to the walking state
                zombie.GetComponent<Animator>().SetLayerWeight(2, 0);
                startUpdate = false;
            }
        }
    }
}
