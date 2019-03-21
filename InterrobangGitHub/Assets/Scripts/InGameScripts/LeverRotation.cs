using UnityEngine;
using System.Collections;

public class LeverRotation : MonoBehaviour
{
    CameraPan cameraPan;
    GameObject ghost;
    public GameObject rotationObject;
    bool ghostIsInside = false;
    bool isLeverOn = false;
    int clickCounter = 0;

    Animator anim;

    //rotation vars
    public float rotationAmount = 90;
    public bool rotateLeft = true;
    [Range (0,100)]
    public float rotationSpeed = 0.5f;
    //the amount of rotation needed for the object 
    float initialRotation;
    

    //animation vars
    bool runTimer = false;
    bool rotationUpdate = false;
    private Vector2 ghostPosWhenLeverOn;
    float animTimer;
    float turnGhostOff = 1;
    Animator ghostAnim;

    ParticleSystem particle;
    SwitchingCharacters characters;
    
    float rotationSum;

    [HideInInspector]
    public bool pan;

    private void Start()
    {
        //set up the ghost game object
        ghost = GameObject.FindGameObjectWithTag("ghost");
        ghostAnim = ghost.GetComponent<Animator>();

        //gets the particle system off of the lever and sets it to false
        particle = gameObject.GetComponent<ParticleSystem>();
        characters = GameObject.FindObjectOfType<SwitchingCharacters>();
        particle.enableEmission = false;
      

        //stores the initial rotation so it can be set back to it 
        initialRotation = rotationObject.GetComponent<Transform>().eulerAngles.z;

        rotationSum = rotationAmount;

        anim = gameObject.GetComponent<Animator>();

        cameraPan = gameObject.GetComponent<CameraPan>();
        gameObject.GetComponent<CameraPan>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks to see if its the ghost beside the lever
        if (collision.tag == "ghost")
        {
            ghostIsInside = true;
          
            gameObject.GetComponent<CameraPan>().enabled = true;
            anim.SetBool("Interactable", true);
        }


    }

    //when the ghost leaves the collider set it back to false 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ghost")
        {
            ghostIsInside = false;
            //allows the player to know when it can be interacted with 
         
            gameObject.GetComponent<CameraPan>().enabled = false;
            anim.SetBool("Interactable", false);
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
                    rotationUpdate = true;
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
            RotationUpdateOff();
            anim.SetBool("leverState", false);
        }

        if (runTimer == true)
        {
            animTimer += 1 * Time.deltaTime;
        }

        if (rotationUpdate == true)
        {
            RotationUpdateOn();
        }

        if (anim.GetBool("possess") == true)
        {
            ghost.transform.position = ghostPosWhenLeverOn;
        }

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
                ghostAnim.SetBool("possess", true);
                runTimer = true;
                ghostPosWhenLeverOn = ghost.transform.position;
                particle.enableEmission = true;
                isLeverOn = true;
            }

            //if the lever is pressed again it is off so rotate back and turn the emission off 
            if (clickCounter == 2 && characters.counter == 2)
            {
                ghostAnim.SetBool("possess", false);
                particle.enableEmission = false;
                rotationUpdate = false;
                isLeverOn = false;
            }
        }
    }

    IEnumerator wait()
    {
        if (pan)
        {
            cameraPan.objectPan = rotationObject;
            cameraPan.panToObject = true;
        }

        yield return new WaitForSeconds(cameraPan.timeBeforePanToObject);

        if (rotateLeft && rotationAmount > 0)
        {
            rotationObject.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            rotationAmount -= rotationSpeed * Time.deltaTime;
        }

        if (!rotateLeft && rotationAmount > 0)
        {
            rotationObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            rotationAmount -= rotationSpeed * Time.deltaTime;
        }
    }
    private void RotationUpdateOn()
    {
        StartCoroutine(wait());      
    }

    private void RotationUpdateOff()
    {
        cameraPan.panToObject = false;
        pan = true;

        if (rotateLeft && rotationAmount < rotationSum)
        {
            rotationObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            rotationAmount += rotationSpeed * Time.deltaTime;
        }

        if (!rotateLeft && rotationAmount < rotationSum)
        {
            rotationObject.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            rotationAmount += rotationSpeed * Time.deltaTime;
        }
    }
}
