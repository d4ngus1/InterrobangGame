using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveOntoNextScene : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
    
        //if the user taps the screen unity will load the next scene in the list 
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(1);
        }
	
	}
}
