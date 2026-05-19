using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameController : MonoBehaviour
{
    public ScenarioPlayer scenarioPlayer;
    private bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject loseMenu;

    public static event Action<bool> onGamePaused;

    void Start()
    {
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

    void Pause() 
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
        isPaused = true;
        onGamePaused?.Invoke(isPaused);
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
