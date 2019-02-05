using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    [HideInInspector]
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
            cameraSwitch(objectPan.transform);
            StartCorotine();
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

        //all elevators
        var elevator = GameObject.FindObjectsOfType<LeverElevator>();
        for (int i = 0; i < elevator.Length; i++)
        {
            elevator[i].pan = false;
        }

        //all lever rotations
        var leverRotation = GameObject.FindObjectsOfType<LeverRotation>();
        for (int i = 0; i < leverRotation.Length; i++)
        {
            leverRotation[i].pan = false;
        }

        //all pressure pads
        var pressurePads = GameObject.FindObjectsOfType<PressurePadScript>();
        for (int i = 0; i < pressurePads.Length; i++)
        {
            pressurePads[i].pan = false;
        }

    }

    public void cameraSwitch(Transform objectTransform)
    {
        cameraFollowPlayer.lockedToPlayers = false;
        cameraFollowPlayer.CameraSwitch(objectTransform, false);
    }
}
