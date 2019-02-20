using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TutorialVideoObjectScript : MonoBehaviour
{
    public VideoPlayer videoP;
    public RawImage rawImage;
    private bool characterNear;

    // Use this for initialization
    void Start()
    {
        videoP.Prepare();
        rawImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks when the video has finished playing
        if ((ulong)videoP.frame == videoP.frameCount)
        {
            videoP.Stop();
            videoP.Prepare();
            rawImage.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (characterNear)
        {
            rawImage.enabled = true;
            //when the user has pressed the icon start the video
            videoP.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        characterNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        characterNear = false;
    }

    
}
