using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour {

    public Animator anim;
    public string ifNotButtonSceneName;
    public bool tapToChangeScene;

    private void Update()
    {
        if(tapToChangeScene)
        {
            if(Input.GetMouseButton(0))
            {
                StartCoroutine(LoadScene(ifNotButtonSceneName));
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadScene(ifNotButtonSceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        anim.SetTrigger("Fade Out");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
