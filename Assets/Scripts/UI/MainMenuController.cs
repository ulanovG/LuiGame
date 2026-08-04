using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public ScenariosSelect scenariosSelect;

    void Start()
    {
        Time.timeScale = 1f;
    }
    
    public void LoadGameScene()
    {
        GameManager.Instance.scenario = scenariosSelect.GetScenario();
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
