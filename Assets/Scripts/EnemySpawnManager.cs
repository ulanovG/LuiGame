using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public WhipController whipController;
    public GameObject enemyContainer;
    public List<GameObject> spawns;

    public void SpawnEnemy(GameObject enemy, string spawnName)
    {
        if(spawns.FirstOrDefault(s => s.name == spawnName) is GameObject spawn)
        {
            var enemyInstance = Instantiate(enemy, spawn.transform.position, new Quaternion(), enemyContainer.transform);
            var enemyController = enemyInstance.GetComponent<EnemyController>();
            enemyController.target = target;
            enemyController.player = player;
            enemyController.whipController = whipController;
        }
        else
        {
            Debug.LogWarning("Spawn name " + spawnName + " not found. Enememy skipped");
        }
    }
}
