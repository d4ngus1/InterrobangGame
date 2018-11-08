using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Button playButton, optionsButton;

    // Update is called once per frame
    void Update()
    {
        //if the play button is pressed then move onto the next scene
        playButton.onClick.AddListener(Play);


        //if the options button is pressed then move onto the options scene
        optionsButton.onClick.AddListener(Options);
    

    }

    void Play()
    {
        SceneManager.LoadScene(2);
    }

    void Options()
    {
        SceneManager.LoadScene(3);
    }
}
