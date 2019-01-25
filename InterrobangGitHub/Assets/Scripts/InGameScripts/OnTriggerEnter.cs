using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnTriggerEnter : MonoBehaviour
{
    public bool zombieTrigger, ghostTrigger;
    public Text text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            zombieTrigger = true;
            text.text = "zombie!";
        }
        if (collision.tag == "ghost")
        {
            ghostTrigger = true;
            text.text = "ghost!";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            zombieTrigger = false;
        }
        if (collision.tag == "ghost")
        {
            ghostTrigger = false;
        }
    }

    public void Reset()
    {
        zombieTrigger = false;
        ghostTrigger = false;
    }
}
