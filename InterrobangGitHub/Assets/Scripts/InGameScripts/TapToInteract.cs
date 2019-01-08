using UnityEngine;
using System.Collections;

public class TapToInteract : MonoBehaviour
{
    public bool platformTapped;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //find all the crumble platforms 
        var crumblePlatforms = GameObject.FindObjectsOfType<PlatformCrumble>();
        for (int i = 0; i < crumblePlatforms.Length; i++)
        {
            //if the zombie is within the platform then allow it to be tapped
            if (crumblePlatforms[i].platformCanBreak == true)
            {
                platformTapped = true;
            }
        }

    }
}

