using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    public GameObject objectPan;
    public bool panToObject;
    public float timeToSwitchBackToCamera;
    Vector3 currentPosition, finishedPosition;
    CameraFollowPlayer cameraFollowPlayer;

    // Use this for initialization
    void Start()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollowPlayer = GameObject.FindObjectOfType<CameraFollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panToObject)
        {
            cameraSwitch();
        }
        else
        {
            cameraFollowPlayer.lockedToPlayers = true;
        }

    }

    public void StartCorotine()
    {
        StartCoroutine(waitTime());
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(timeToSwitchBackToCamera);
        panToObject = false;
    }

    public void cameraSwitch()
    {
        cameraFollowPlayer.lockedToPlayers = false;
        cameraFollowPlayer.CameraSwitch(objectPan.GetComponent<Transform>(), false);
    }
}
