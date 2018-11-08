using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    ZombieMovement zombie;
    bool ladderClimbable = false;
    public GameObject highlight;

    // Use this for initialization
    void Start()
    {
        zombie = ZombieMovement.FindObjectOfType<ZombieMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when the player gets within the collider the ladder becomes climbable 
        if (collision.tag == "zombie")
        {
            ladderClimbable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //when the player leaves the collider ladder resets
        if (collision.tag == "zombie")
        {
            ladderClimbable = false;
            zombie.onLadder = false;
        }
    }

    //only works when the player is whithin the collider, make the zombie ladder on and highlight off
    private void OnMouseDown()
    {
        if (ladderClimbable == true)
        {
            zombie.onLadder = true;
            highlight.SetActive(false);
        }
    }
}
