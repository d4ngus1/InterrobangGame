using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    [HideInInspector]
    public GameObject objectPan;
    [HideInInspector]
    public bool panToObject;
    public bool panToZombie = false;
    public float timeBeforePanToObject = 0.5f;
    public float timeToSwitchBackToCamera;
    Vector3 currentPosition, finishedPosition;
    CameraFollowPlayer cameraFollowPlayer;
    SwitchingCharacters switchingCharacters;
    ZombieMovement zombie;
    GhostMovement ghost;

    private bool beingHandled;
    bool followObject;
    
    // Use this for initialization
    void Start()
    {
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollowPlayer = GameObject.FindObjectOfType<CameraFollowPlayer>();
        switchingCharacters = GameObject.FindObjectOfType<SwitchingCharacters>();
        zombie = GameObject.FindObjectOfType<ZombieMovement>();
        ghost = GameObject.FindObjectOfType<GhostMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panToObject && !beingHandled)
        {
            StartCores();
        }

        if (panToObject == false)
        {
            cameraFollowPlayer.lockedToPlayers = true;
        }

        if(followObject)
        {
            cameraSwitch(objectPan.transform);
        }
    }

    IEnumerator timeBeforePan()
    {
        yield return new WaitForSeconds(timeBeforePanToObject);
        followObject = true;
        StartCoroutine(waitTime());
    }

    public void StartCores()
    {
        StartCoroutine(timeBeforePan());      
    }

    IEnumerator waitTime()
    {
        beingHandled = true;

        yield return new WaitForSeconds(timeToSwitchBackToCamera);

        panToObject = false;
        followObject = false;

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
        beingHandled = false;

        //allows the option to pan to the zombie or to the ghost 
        if (panToZombie)
        {
            //allows the camera to pan back to the zombie after the possession pan 
            cameraFollowPlayer.zombieActivated = true;
            switchingCharacters.counter = 1;
            zombie.active = true;
            ghost.active = false;
        }

        if (!panToZombie)
        {
            //allows the camera to pan back to the zombie after the possession pan 
            cameraFollowPlayer.zombieActivated = false;
            switchingCharacters.counter = 2;
            ghost.active = true;
            zombie.active = false;
        }
    }

    public void cameraSwitch(Transform objectTransform)
    {
        cameraFollowPlayer.lockedToPlayers = false;
        cameraFollowPlayer.CameraSwitch(objectTransform, false);
    }
}
