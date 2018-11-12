using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CollectableScript : MonoBehaviour
{
    private bool floatUp = true;
    private Vector3 floatChange;
    private Animator anim;   
    private bool startTimer = false;
    private float Totaltime;

    public GameObject fullCollectableScreen;
    public float floatAmount;
    public GameObject ImagePiece;
    public float timeToCloseCollectables = 10;
    public int placementInAnimator;
    public int totalAmountOfPieces;

    private void Start()
    {
        //sets the new transform to the position of the object
        floatChange.x = gameObject.transform.position.x;
        floatChange.y = gameObject.transform.position.y;
        floatChange.z = gameObject.transform.position.z;
        //starts the coroutine function
        StartCoroutine(FloatObject());
        //define the animtor
        anim = ImagePiece.GetComponent<Animator>();
        //set the collectable screen to not be visable
        fullCollectableScreen.SetActive(false);
    }

    void Update()
    {
        FloatMovement();

        if (startTimer == true)
        {
            Timer();
        }
    }

    private void Timer()
    {
        //set the scale back up 
        fullCollectableScreen.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        Totaltime += 1 * Time.deltaTime;

        if (Totaltime > timeToCloseCollectables)
        {
            //scale the collectable screen back down to zero
            fullCollectableScreen.transform.localScale = Vector3.zero;

            //get rid of the collectable now that its been collected
            Destroy(gameObject);
        }
    }

    private void FloatMovement()
    {
        if (floatUp == true)
        {
            floatChange.y += floatAmount;
            gameObject.transform.position = floatChange;
        }
        if (floatUp == false)
        {
            floatChange.y -= floatAmount;
            gameObject.transform.position = floatChange;
        }
    }

    IEnumerator FloatObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            //switch the bool to the other state and then wait 1 second 
            floatUp = !floatUp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set the full screen back to being active
        fullCollectableScreen.SetActive(true);

        //set the new piece to the front 
        ImagePiece.transform.SetSiblingIndex(totalAmountOfPieces);

        //start the animation
        anim.SetBool("collected", true);

        //set the path for it to go down 
        anim.SetInteger("placement", placementInAnimator);

        //start the timer that takes away the collectable screen
        startTimer = true;
    }
}
