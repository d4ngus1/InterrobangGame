using UnityEngine;
using System.Collections;

public class LockedDoorScript : MonoBehaviour
{

    [HideInInspector]
    public bool keyCollected;

    void Update()
    {
        //if the key has been collected then turn off the collider to let the zombie through 
        if (keyCollected)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "zombie" && keyCollected == true)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Open");
        }
    }
}
