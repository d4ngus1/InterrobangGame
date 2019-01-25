using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerLevelSwapScript : MonoBehaviour {

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(sceneName);
    }
}
