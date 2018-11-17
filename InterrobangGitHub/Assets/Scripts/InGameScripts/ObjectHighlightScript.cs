using UnityEngine;
using System.Collections;

public class HighlightObjectScript : MonoBehaviour
{

    public GameObject highlight; 

    // Use this for initialization
    void Start()
    {
        //boxCollider = gameObject.GetComponent<BoxCollider2D>();
        highlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            highlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            highlight.SetActive(false);
        }
    }
}
