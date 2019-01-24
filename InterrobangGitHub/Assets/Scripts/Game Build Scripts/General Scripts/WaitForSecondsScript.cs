using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaitForSecondsScript : MonoBehaviour
{
    public bool sceneSwitch;
    public string sceneToSwitchToName;
    public int timeToWait;

    void Start()
    {
        if(sceneSwitch)
        {
            StartCoroutine(SceneSwitch(sceneToSwitchToName));
        }
    }

    IEnumerator SceneSwitch(string sceneName)
    {
        if (sceneSwitch)
        {
            yield return new WaitForSeconds(timeToWait);
            SceneManager.LoadScene(sceneName);
        }
    }
}
