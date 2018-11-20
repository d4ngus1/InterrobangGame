using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FreeCam : MonoBehaviour
{
    public Button button;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    Vector3 touchStart;
    bool zoomed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 0)
        {
            zoomed = false;
        }

        if (Input.touchCount == 2)
        {
            zoomed = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 tZeroPastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 tOnePastPos = touchOne.position - touchOne.deltaPosition;

            float pastMag = (tZeroPastPos - tOnePastPos).magnitude;
            float currentMag = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMag - pastMag;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            if (zoomed == false)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
            }
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom(float zoomAmount)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, zoomOutMin, zoomOutMax);
    }
    private Vector2 GetWorldPosFinger(int fingerNum)
    {
        return gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(fingerNum).position);
    }

    private Vector2 GetWorldPos()
    {
        return gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
}
