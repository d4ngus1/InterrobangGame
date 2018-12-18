using UnityEngine;
using System.Collections;

public class DoorHealth : MonoBehaviour
{

    public float doorHealth = 1;
    Vector3 healthScale;
    public bool doorWillTakeDamage = false;
    public GameObject healthBar;
    public GameObject door;
    public Animator anim;
    public BoxCollider2D boxCollider;
    public GameObject highlight;

    // Use this for initialization
    void Start()
    {
        //sets the health scale to the size of the health bar in the scene
        healthScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //sets the scale of the health to how much health the door has left
        healthScale.x = doorHealth;
        healthBar.transform.localScale = healthScale;

        //door health is at 2
        if (doorHealth == 1)
        {
            anim.SetInteger("Door Health", 2);
        }

        //door health is at 1
        if (doorHealth == 0.5f)
        {
            anim.SetInteger("Door Health", 1);
        }

        //door health is 0
        if (doorHealth <= 0)
        {
            //turn off the collider and also set the highlight to false
            anim.SetInteger("Door Health", 0);
            boxCollider.enabled = false;
            highlight.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            doorWillTakeDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            doorWillTakeDamage = false;
        }
    }
}
