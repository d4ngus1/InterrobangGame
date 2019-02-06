using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{
    public GameObject keyDoor;

    bool keyCollected; 

    void Update()
    {
        //when the key has been collected notify the door script to turn off its collider 
        if(keyCollected)
        {
            keyDoor.GetComponent<LockedDoorScript>().keyCollected = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "zombie")
        {
            keyCollected = true;            
        }
    }
}
