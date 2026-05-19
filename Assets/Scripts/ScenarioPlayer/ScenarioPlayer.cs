using System.Collections;
using UnityEngine;

public class ScenarioPlayer : MonoBehaviour
{
    public spScenario scenario;
    public EnemySpawnManager enemySpawnManager;

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
