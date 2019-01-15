using UnityEngine;
using System.Collections;

public class TapToInteract : MonoBehaviour
{
    public bool platformTapped;
    public bool meleeTapped;

    private void OnMouseDown()
    {
        //find all the crumble platforms 
        var crumblePlatforms = GameObject.FindObjectsOfType<PlatformCrumble>();
        //find all the melee items 
        var meleeAssets = GameObject.FindObjectsOfType<DoorHealth>();

        //loops through all the platforms 
        for (int i = 0; i < crumblePlatforms.Length; i++)
        {
            //if the zombie is within the platform then allow it to be tapped
            if (crumblePlatforms[i].platformCanBreak == true)
            {
                platformTapped = true;
            }
        }

        //loops through all the melee assets
        for (int i = 0; i < meleeAssets.Length; i++)
        {
            //if the zombie is within range of the melee asset for it to take damage
            if (meleeAssets[i].doorWillTakeDamage == true)
            {
                meleeTapped = true;
            }
        }
    }
}

