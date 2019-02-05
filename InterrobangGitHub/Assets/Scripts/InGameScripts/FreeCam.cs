using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FreeCam : MonoBehaviour
{
    public float minimumZoomOut = 3.6f;
    public float maximumZoomOut = 8;
    Vector3 startingTouchPos;
    bool zoomActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //set up the position of a single touch
            startingTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 0)
        {
            //if there isnt any touches then zoom cant be happening
            zoomActive = false;
        }

        if (Input.touchCount == 2)
        {
            //two fingers are on screen
            zoomActive = true;

            //set up touches
            Touch fingerZero = Input.GetTouch(0);
            Touch fingerOne = Input.GetTouch(1);

            //last positions when it has moved
            Vector2 fingerZeroLastPosition = fingerZero.position - fingerZero.deltaPosition;
            Vector2 fingerOneLastPosition = fingerOne.position - fingerOne.deltaPosition;

            //mag calculations
            float prevMagnitude = (fingerZeroLastPosition - fingerOneLastPosition).magnitude;
            float currentMagnitude = (fingerZero.position - fingerOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            if (zoomActive == false)
            {
                Vector3 direction = startingTouchPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
            }
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom(float zoomAmount)
    {
        //sets the orthographic size which is zoom
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minimumZoomOut, maximumZoomOut);
    }
}
