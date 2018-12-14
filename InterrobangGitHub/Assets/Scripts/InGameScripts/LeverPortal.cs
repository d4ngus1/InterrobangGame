using UnityEngine;
using System.Collections;

public class LeverPortal : MonoBehaviour
{

    GameObject ghost;
    public GameObject highlight;
    bool ghostIsInside = false;
    public bool isLeverOn = false;
    int clickCounter = 0;
    ParticleSystem particle;
    SwitchingCharacters characters;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        ghost = GameObject.FindGameObjectWithTag("ghost");
        //gets the particle system off of the lever and sets it to false
        particle = gameObject.GetComponent<ParticleSystem>();
        characters = GameObject.FindObjectOfType<SwitchingCharacters>();
        particle.enableEmission = false;
        highlight.SetActive(false);
        anim = gameObject.GetComponent<Animator>();    
            }


    // Update is called once per frame
    void Update()
    {


        if (ghostIsInside == true)
        {
            //when the leaver is on stop the ghost from being controlled as it is now possessing the switch 
            if (isLeverOn == true)
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = -5;
                ghost.SetActive(false);
                anim.SetBool("leverState", true);
                highlight.SetActive(false);
            }
            if (isLeverOn == false)
            {
                ghost.GetComponent<SpriteRenderer>().sortingOrder = 1;
                ghost.SetActive(true);
                anim.SetBool("leverState", false);
            }
        }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ghost")
        {
            ghostIsInside = false;
            //allows the player to know when it can be interacted with 
            highlight.SetActive(false);
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
                //transform.Rotate(new Vector3(0, 0, 100));
                
                particle.enableEmission = true;
                isLeverOn = true;
            }

            //if the lever is pressed again it is off so rotate back and turn the emission off 
            if (clickCounter == 2 && characters.counter == 2)
            {
                //transform.Rotate(new Vector3(0, 0, -100));
                //anim.SetBool("leverState", false);
                particle.enableEmission = false;

                isLeverOn = false;
            }
        }

    }
}
