using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Button button;
    bool freeCamOn = false;

    void Update()
    {
        //switching between the free cam and the normal cam 
        if (freeCamOn == true)
        {
            gameObject.GetComponent<CameraFollowPlayer>().enabled = false;
            gameObject.GetComponent<FreeCam>().enabled = true;
        }
        if (freeCamOn == false)
        {
            Camera.main.orthographicSize = 3;
            gameObject.GetComponent<CameraFollowPlayer>().enabled = true;
            gameObject.GetComponent<FreeCam>().enabled = false;
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
