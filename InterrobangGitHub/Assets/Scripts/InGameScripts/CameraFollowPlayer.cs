using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //what the camera will be following
    public Transform targetZombie;
    public Transform targetGhost;

    public float smoothSpeed = 1;  //how fast the camera will react to the moving object 
    public bool zombieActivated = true, lockedToPlayers = true;

    Vector3 desiredPosition;
    Vector3 offset; //the offset of the camera to the player

    private void Update()
    {
        if (lockedToPlayers)
        {
            //switch between what the camera will aim to follow
            if (zombieActivated) CameraSwitch(targetZombie, true);
            else CameraSwitch(targetGhost, false);
        }
    }

    //passes in the selected player and moves the camera to that players position 
    public void CameraSwitch(Transform selectedPlayer, bool zombie)
    {
        if (zombie)
        {
            offset = targetZombie.GetComponent<ZombieMovement>().cameraOffset;
        }
        else offset = targetGhost.GetComponent<GhostMovement>().cameraOffset;

        //add the offset to the camera position
        desiredPosition = selectedPlayer.position + offset;

        //lerp to the new position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
