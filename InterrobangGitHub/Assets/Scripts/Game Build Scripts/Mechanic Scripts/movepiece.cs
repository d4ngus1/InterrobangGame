using UnityEngine;
using System.Collections;

public class movepiece : MonoBehaviour {


    public string pieceStatus = "idle";

    public Transform edgeParticles;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (pieceStatus == "pickedup")
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
	}


    void OnTriggerEnter2D(Collider2D other)
    {
     if (other.gameObject.name == gameObject.name)
        {
			Instantiate(edgeParticles, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z), edgeParticles.rotation);
            transform.position = other.gameObject.transform.position;
            pieceStatus = "dropped";
        }


    }

    void OnMouseDown()
    {
        pieceStatus = "pickedup";  
    }

    void OnMouseUp()
    {
        pieceStatus = "dropped"; 
    }
}
