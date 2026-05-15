using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGameScene()
    {
        GameManager.Instance.scenario = "test";
        StartCoroutine(LoadGameSceneAsync());
    }

    IEnumerator LoadGameSceneAsync()
    {
        var load = SceneManager.LoadSceneAsync("GameScene");

        while (!load.isDone)
        {
            Debug.Log(load.progress);
            yield return null;
        }
    }
}
