using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Serializable]
    public struct ActiveLevel
    {
        public string key;
        public spScenario spScenario;
    }

    public string scenario;
    public List<ActiveLevel> activeScenarios;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
