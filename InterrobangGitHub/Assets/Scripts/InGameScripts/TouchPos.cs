using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchPos : MonoBehaviour {

    Text text;
    public bool x = true;

	// Use this for initialization
	void Start ()
    {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (x)
            text.text = "X: " + Input.mousePosition.x.ToString();
        else text.text = "Y: " + Input.mousePosition.y.ToString();
    }
}
