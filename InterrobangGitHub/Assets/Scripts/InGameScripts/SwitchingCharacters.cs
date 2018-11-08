using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SwitchingCharacters : MonoBehaviour
{

    public Button characterSwitchButton;

    private ZombieMovement zombieMovement;
    private GhostMovement ghostMovement;
    private CameraFollowPlayer camera;

    public int counter = 1;

    private void Awake()
    {
        zombieMovement = GameObject.FindObjectOfType<ZombieMovement>();
        ghostMovement = GameObject.FindObjectOfType<GhostMovement>();
        camera = GameObject.FindObjectOfType<CameraFollowPlayer>();
    }

    private void Start()
    {
        zombieMovement.active = true;
    }

    // Update is called once per frame
    void OnEnable()
    {
        //waits for the button to be pressed before switching the characters
        characterSwitchButton.onClick.AddListener(SwitchCharacter);
    }

    void SwitchCharacter()
    {
        counter++;

        //reset the counter
        if (counter > 2) counter = 1;

        //switches between the characters
        if (counter == 1)
        {
            zombieMovement.active = true;
            camera.zombieActivated = true;
            ghostMovement.active = false;
        }
        if (counter == 2)
        {
            ghostMovement.active = true;
            camera.zombieActivated = false;
            zombieMovement.active = false;
        }
        
    }
}
