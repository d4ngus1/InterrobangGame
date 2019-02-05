using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Button button;
    public bool freeCamOn = false;
    GameObject ghost, zombie;

    int caseSwitch = 2;

    private void Start()
    {
        ghost = GameObject.FindGameObjectWithTag("ghost");
        zombie = GameObject.FindGameObjectWithTag("zombie");
    }
    void Update()
    {

        switch (caseSwitch)
        {
            case 1:
                Camera.main.orthographicSize = Camera.main.orthographicSize + 1;
                break;
                
            case 2:
                Camera.main.orthographicSize = 3.6f;
                break;
        }

        //switching between the free cam and the normal cam 
        if (freeCamOn == true)
        {
            gameObject.GetComponent<CameraFollowPlayer>().enabled = false;
            gameObject.GetComponent<FreeCam>().enabled = true;
            ghost.GetComponent<GhostMovement>().enabled = false;
            zombie.GetComponent<ZombieMovement>().enabled = false;
            zombie.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            //make sure that the idle animation is playing for the zombie and the ghost
            zombie.GetComponent<Animator>().SetFloat("speed", 0.0f);
            ghost.GetComponent<Animator>().SetFloat("gSpeed", 0.0f);

            caseSwitch = 1;
            
        }
        if (freeCamOn == false)
        {
            caseSwitch = 2;
            gameObject.GetComponent<CameraFollowPlayer>().enabled = true;
            gameObject.GetComponent<FreeCam>().enabled = false;
            ghost.GetComponent<GhostMovement>().enabled = true;
            zombie.GetComponent<ZombieMovement>().enabled = true;
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(CameraSwitch);
    }

    void CameraSwitch()
    {
        freeCamOn = !freeCamOn;
    }
}
