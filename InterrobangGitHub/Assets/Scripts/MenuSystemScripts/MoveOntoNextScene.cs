using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveOntoNextScene : MonoBehaviour
{

    public string sceneName;

    void Update()
    {
        //will load the screen if the mouse or a tap is clicked on the screen  
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}
