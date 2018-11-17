using UnityEngine;
using System.Collections;

public class TargetHighlightScript : MonoBehaviour
{
    public GameObject targetScript;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            targetScript.GetComponent<ZombieHeadThrow>().zombieIsInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            targetScript.GetComponent<ZombieHeadThrow>().zombieIsInside = false;
        }
    }
}
