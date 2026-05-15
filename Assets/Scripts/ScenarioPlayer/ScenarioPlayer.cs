using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ScenarioPlayer : MonoBehaviour
{
    public spScenario scenario;
    public EnemySpawnManager enemySpawnManager;

    //
    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.scenario))
            {
                var selectedScenario = GameManager.Instance.activeScenarios.FirstOrDefault(s => s.key == GameManager.Instance.scenario).spScenario;
                if (selectedScenario != null)
                {
                    scenario = selectedScenario;
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
        PlayScenario();
    }

    public void PlayScenario()
    {
        if (scenario == null || scenario.waves.Count == 0)
        {
            Debug.LogError("Scenario is empty");
            return;
        }

        StartCoroutine(ScenarioCoroutine());
    }

    IEnumerator ScenarioCoroutine()
    {
        foreach(var wave in scenario.waves)
        {
            for(int i = 0; i < wave.repeat; i++)
            {
                yield return new WaitForSeconds(wave.delay);

                foreach(var groupObject in wave.groups)
                {
                    var group = groupObject as IspGroup;
                    foreach (var mobSpawn in group.MobSpawns)
                    {
                        enemySpawnManager.SpawnEnemy(mobSpawn.enemy, mobSpawn.spawnName);
                    }
                }
            }
        }
        
    }
}
