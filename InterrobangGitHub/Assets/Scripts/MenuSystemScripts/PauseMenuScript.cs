using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;
    Button pauseButton;
    ZombieMovement zombie;
    bool pauseMenuOpen;

    // Use this for initialization
    void Start()
    {
        pauseButton = this.GetComponent<Button>();
        pauseMenu.SetActive(false);
        zombie = GameObject.FindObjectOfType<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenuOpen)
        {
            zombie.rb.velocity = Vector2.zero;
        }
    }

    public void ButtonPressed()
    {
        pauseMenu.SetActive(true);
        pauseMenuOpen = true;
        zombie.active = false;   
    }

    public void ButtonExit()
    {
        pauseMenu.SetActive(false);
        zombie.enabled = true;
        pauseMenuOpen = false;
    }
}
