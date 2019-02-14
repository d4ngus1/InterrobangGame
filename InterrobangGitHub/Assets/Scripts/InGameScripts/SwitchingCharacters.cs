using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SwitchingCharacters : MonoBehaviour
{
    GameObject stompButton, meleeButton;
    public Button characterSwitchButton;
    private ZombieMovement zombieMovement;
    private GhostMovement ghostMovement;
    private CameraFollowPlayer camera;
    GameObject zombie;

    public int counter = 1;

    [HideInInspector]
    public int amountOfCharacterSwitches;
    [HideInInspector]
    public bool charactersCanMove;

    private void Awake()
    {
        zombieMovement = GameObject.FindObjectOfType<ZombieMovement>();
        ghostMovement = GameObject.FindObjectOfType<GhostMovement>();
        camera = GameObject.FindObjectOfType<CameraFollowPlayer>();
        stompButton = GameObject.FindGameObjectWithTag("stomp");
        meleeButton = GameObject.FindGameObjectWithTag("melee");
        zombie = GameObject.FindGameObjectWithTag("zombie");
    }

    private void Start()
    {
        zombieMovement.active = true;
        charactersCanMove = true;
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
        amountOfCharacterSwitches++;

        //reset the counter
        if (counter > 2) counter = 1;

        //switches between the characters
        if (counter == 1)
        {
            zombieMovement.active = true;
            camera.zombieActivated = true;
            ghostMovement.active = false;
            //stompButton.GetComponent<Animator>().SetBool("zombieActive", true);
            //meleeButton.GetComponent<Animator>().SetBool("zombieActive", true);
        }
        if (counter == 2)
        {
            zombie.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ghostMovement.active = true;
            camera.zombieActivated = false;
            zombieMovement.active = false;
            //stompButton.GetComponent<Animator>().SetBool("zombieActive", false);
            //meleeButton.GetComponent<Animator>().SetBool("zombieActive", false);
        }

    }

    //functions for the event trigger to stop the characters being able to move when the button is being held
    public void StopMovement()
    {
        charactersCanMove = false;
    }

    public void ResumeMovement()
    {
        charactersCanMove = true;
    }
}
