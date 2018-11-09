using UnityEngine;
using System.Collections;

public class LeverRotation : MonoBehaviour
{
    GameObject ghost;
    public GameObject highlight;
    public GameObject rotationObject;
    bool ghostIsInside = false;
    bool isLeverOn = false;
    int clickCounter = 0;
    
    //rotation vars
    [Range (0,1)]
    public float rotationSpeed = 0.5f;
    //the amount of rotation needed for the object 
    public float maxRotation = 180;
    float rotationPosition;
    float initialRotation;
    public bool rotateLeft = true;

    //animation vars
    bool runTimer = false;
    bool rotationUpdate = false;
    private Vector2 ghostPosWhenLeverOn;
    float animTimer;
    public float turnGhostOff = 1;
    bool leverRotate = false;
    Animator ghostAnim;

    ParticleSystem particle;
    SwitchingCharacters characters;

    private void Start()
    {
        //set up the ghost game object
        ghost = GameObject.FindGameObjectWithTag("ghost");
        ghostAnim = ghost.GetComponent<Animator>();

        //gets the particle system off of the lever and sets it to false
        particle = gameObject.GetComponent<ParticleSystem>();
        characters = GameObject.FindObjectOfType<SwitchingCharacters>();
        particle.enableEmission = false;
        highlight.SetActive(false);

        //stores the initial rotation so it can be set back to it 
        initialRotation = rotationObject.GetComponent<Transform>().eulerAngles.z;
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
                ghost.transform.position = ghostPosWhenLeverOn;

                if (animTimer > turnGhostOff)
                {
                    particle.enableEmission = true;
                    ghost.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    ghost.SetActive(false);
                    leverRotate = true;
                    runTimer = false;
                    rotationUpdate = true;
                    animTimer = 0;
                }
            }
            else
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = 1;
                ghost.SetActive(true);
            }
        }

        if (isLeverOn == false)
        {
            RotationUpdateOff();
        }

        if (leverRotate == true)
        {
            transform.Rotate(new Vector3(0, 0, 100));
            leverRotate = false;
        }

        if (runTimer == true)
        {
            animTimer += 1 * Time.deltaTime;
        }

        if (rotationUpdate == true)
        {
            RotationUpdateOn();
        }

        //keeps the highlight behind the lever
        highlight.gameObject.transform.rotation = gameObject.transform.rotation;

        //keeps track of the objects rotation
        //stores the initial rotation of the object
        rotationPosition = rotationObject.GetComponent<Transform>().eulerAngles.z;
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
                ghostPosWhenLeverOn = ghost.transform.position;
                ghostAnim.SetBool("possess", true);
                runTimer = true;
                particle.enableEmission = true;
                isLeverOn = true;
            }

            //if the lever is pressed again it is off so rotate back and turn the emission off 
            if (clickCounter == 2 && characters.counter == 2)
            {
                ghostAnim.SetBool("possess", false);
                transform.Rotate(new Vector3(0, 0, -100));
                particle.enableEmission = false;
                rotationUpdate = false;
                isLeverOn = false;
            }
        }

    }

    private void RotationUpdateOn()
    {
        //rotating the object to the left
        if (rotationPosition > maxRotation && rotateLeft == true)
        {
            rotationObject.transform.Rotate(0, 0, -rotationSpeed);
        }

        //rotating the object to the right 
        if (rotationPosition < maxRotation && rotateLeft == false)
        {
            rotationObject.transform.Rotate(0, 0, rotationSpeed);
        }

    }

    private void RotationUpdateOff()
    {
        //rotating the object back to the right 
        if (rotationPosition < initialRotation && rotateLeft == true)
        {
            rotationObject.transform.Rotate(0, 0, rotationSpeed);
        }

        //rotating the object back to the left
        if (rotationPosition > initialRotation && rotateLeft == false)
        {
            rotationObject.transform.Rotate(0, 0, -rotationSpeed);
        }
    }
}
