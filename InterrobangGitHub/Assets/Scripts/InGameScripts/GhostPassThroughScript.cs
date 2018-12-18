using UnityEngine;
using System.Collections;

public class GhostPassThroughScript : MonoBehaviour {

    GameObject ghost;

	// Use this for initialization
	void Start ()
    {
        ghost = GameObject.FindGameObjectWithTag("ghost");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ignore the collision of the ghost with the gameobject
        Physics2D.IgnoreCollision(ghost.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
	}
}
