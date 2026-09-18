using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class ScenarioPlayer : MonoBehaviour
{
    public spScenario scenario;
    public EnemySpawnManager enemySpawnManager;

    public static event Action onGameWin;

    public void PlayScenario()
    {
        if (scenario == null || scenario.waves.Count == 0)
        {
            Debug.LogError("Scenario is empty");
            return;
        }

        StartCoroutine(ScenarioCoroutine());
        
        if (scenario.time != 0)
        {
            Invoke("WinScenario", scenario.time);
        }
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
                        enemySpawnManager.SpawnEnemy(mobSpawn.enemy, mobSpawn.spawnName, mobSpawn.movementCenterBias);
                    }
                }
            }
        }
        
    }

    public void WinScenario()
    {
        onGameWin?.Invoke();
    }
}
