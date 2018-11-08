using UnityEngine;
using System.Collections;

public class PlatformCrumble : MonoBehaviour
{
    Animator platformAnimation;
    BoxCollider2D boxCollider;
    public GameObject platform;
    public GameObject highlight;
    public bool platformCanBreak = false;
    public bool playAnimation = false;

    // Use this for initialization
    void Start()
    {
        platformAnimation = platform.GetComponent<Animator>();
        boxCollider = platform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation == true)
        {
            platformAnimation.SetBool("crumble", true);
            boxCollider.enabled = false;
            highlight.SetActive(false);
        }
    }

    //when the zombie stands over the platform
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            platformCanBreak = true;
        }
    }

    //when the zombie leaves the platform 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            platformCanBreak = false;
        }
    }
}
