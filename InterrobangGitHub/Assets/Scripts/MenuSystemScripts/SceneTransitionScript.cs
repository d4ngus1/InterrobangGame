using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour {

    public Animator anim1, anim2;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        anim1.SetTrigger("end");
        anim2.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
