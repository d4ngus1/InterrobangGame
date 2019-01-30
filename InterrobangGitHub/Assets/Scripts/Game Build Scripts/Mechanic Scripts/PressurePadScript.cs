using UnityEngine;
using System.Collections;

public class PressurePadScript : MonoBehaviour
{
    public float timeZombieMustBeOnPlatform = 1;
    public bool rotationPad;
    public GameObject rotationObject;
    public bool rotateLeft;
    public float rotationAmount, rotationSpeed = 0.5f;

    public bool elevatorPad;
    public GameObject elevatorObject;
    public float elevatorHeight, elevatorSpeed = 0.05f; 

    Animator anim;
    float activateTimer, storedRotation, storedHeight;
    bool padPressed;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        storedRotation = rotationAmount;
        storedHeight = elevatorObject.GetComponent<Transform>().localPosition.y;
    }

    void Update()
    {
        //checks to see what animation the animator is currently playing 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pressed Idle"))
        {
            padPressed = true;
        }
        else padPressed = false;

        //if its a rotation pad then do the rotation updates
        if (rotationPad)
        {
            RotationUpdate();
        }

        //if its an elevator pad then do the elvator updates
        if (elevatorPad)
        {
            ElevatorUpdate();
        }
    }

    private void ElevatorUpdate()
    {
        if (padPressed)
        {
            //moves the platform up when the platform has been pressed
            if (elevatorObject.transform.localPosition.y < elevatorHeight)
            {
                elevatorObject.transform.Translate(0, elevatorSpeed, 0);
            }
        }
        else
        {
            //returns the platform back to the original position 
            if (elevatorObject.transform.localPosition.y > storedHeight)
            {
                elevatorObject.transform.Translate(0, -elevatorSpeed, 0);
            }
        }
    }

    private void RotationUpdate()
    {
        if (padPressed)
        {
            //starts the rotation when the pad has been pressed
            if (rotateLeft && rotationAmount > 0)
            {
                rotationObject.transform.Rotate(0, 0, -rotationSpeed);
                rotationAmount -= rotationSpeed;
            }

            if (!rotateLeft && rotationAmount > 0)
            {
                rotationObject.transform.Rotate(0, 0, rotationSpeed);
                rotationAmount -= rotationSpeed;
            }
        }
        else
        {
            //when the pad hasnt been pressed rotates back to original position
            if (rotateLeft && rotationAmount < storedRotation)
            {
                rotationObject.transform.Rotate(0, 0, rotationSpeed);
                rotationAmount += rotationSpeed;
            }

            if (!rotateLeft && rotationAmount < storedRotation)
            {
                rotationObject.transform.Rotate(0, 0, -rotationSpeed);
                rotationAmount += rotationSpeed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //makes sure its the zombie and not the ghost
        if (collision.tag == "zombie")
        {
            //give the zombie a chance to be on the platform before the pad changes
            activateTimer += 3 * Time.deltaTime;

            if (activateTimer > timeZombieMustBeOnPlatform)
            {
                //zombie is now on the pad so play the animation 
                anim.SetBool("Pressed", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //makes sure its the zombie and not the ghost
        if (collision.tag == "zombie")
        {
            //zombie has left the pad so play the animation 
            anim.SetBool("Pressed", false);
            //reset the timer for the next time the zombie is on the platform 
            activateTimer = 0;
        }
    }
}
