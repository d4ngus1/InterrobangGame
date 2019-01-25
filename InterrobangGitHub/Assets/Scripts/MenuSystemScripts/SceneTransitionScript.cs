using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour {

    public Animator anim1, anim2;
    public string ifTriggerSceneName;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadScene(ifTriggerSceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        anim1.SetTrigger("end");
        anim2.SetTrigger("end");
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(sceneName);
    }
}
