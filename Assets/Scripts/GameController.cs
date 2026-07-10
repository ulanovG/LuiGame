using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public ScenarioPlayer scenarioPlayer;
    private bool isPaused = false;

    public bool debug = false;

    public ScoreController scoreController;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public TextMeshProUGUI scoreText;

    public static event Action<bool> onGamePaused;

    void Start()
    {
        Time.timeScale = 1f;

        TargetLightController.onGameLose += LoseGame;

        if (GameManager.Instance != null)
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.scenario))
            {
                var selectedScenario = GameManager.Instance.activeScenarios.FirstOrDefault(s => s.key == GameManager.Instance.scenario).spScenario;
                if (selectedScenario != null)
                {
                    scenarioPlayer.scenario = selectedScenario;
                    Debug.Log("Star scenario " + GameManager.Instance.scenario);
                }
                else
                {
                    Debug.LogError("Scenario not found");    
                }
            }
            else
            {
                Debug.LogError("Scenario key empty");
            }
        }
        else
        {
            Debug.LogError("Game Manager missing");
        }
        scenarioPlayer.PlayScenario();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
                Resume();
            else 
                Pause();
        }
    }

    public void Pause() 
    {
        isPaused = true;
        onGamePaused?.Invoke(isPaused);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume() 
    {
        isPaused = false;
        onGamePaused?.Invoke(isPaused);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void LoseGame()
    {
        if(!debug)
        {
            isPaused = true;

            onGamePaused?.Invoke(isPaused);
            scoreController.gameObject.SetActive(false);
            scoreText.text = scoreController.Score.ToString();
            loseMenu.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("LOSE");
        }
    }

    public void LoadMenuScene()
    {
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }
    public void ReloadGameScene()
    {
        StartCoroutine(LoadSceneAsync("GameScene"));
    }

    IEnumerator LoadSceneAsync(string name)
    {
        var load = SceneManager.LoadSceneAsync(name);

        while (!load.isDone)
        {
            Debug.Log(load.progress);
            yield return null;
        }
    }

    void OnDestroy()
    {
        TargetLightController.onGameLose -= LoseGame;
    }
}
