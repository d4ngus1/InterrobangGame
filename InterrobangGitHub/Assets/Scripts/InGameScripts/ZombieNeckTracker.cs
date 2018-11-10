using UnityEngine;
using System.Collections;

public class ZombieNeckTracker : MonoBehaviour {

    GameObject zombie;

	// Use this for initialization
	void Start ()
    {
        zombie = GameObject.FindGameObjectWithTag("zombie");
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = zombie.transform.position;
	}
}
