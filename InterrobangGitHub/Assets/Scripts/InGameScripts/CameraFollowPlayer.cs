using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //what the camera will be following
    public Transform targetZombie;
    public Transform targetGhost;

    public float smoothSpeed = 1;  //how fast the camera will react to the moving object
    public Vector3 offset; //the offset of the camera to the player
    public bool zombieActivated = true;

    private void Update()
    {
        if (zombieActivated) CameraSwitch(targetZombie);
        else CameraSwitch(targetGhost);
    }

    //passes in the selected player and moves the camera to that players position 
    public void CameraSwitch(Transform selectedPlayer)
    {
        Vector3 desiredPosition = selectedPlayer.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
