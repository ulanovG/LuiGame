using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float delay = 5f;
    public float spawnPeriod = 5f;
    public GameObject target;
    public GameObject player;
    public WhipController whipController;
    public GameObject enemyContainer;
    public GameObject enemy;
    public List<GameObject> spawns;

    void Start()
    {
        InvokeRepeating("SelectSpawn", delay, spawnPeriod);
    }

    void SelectSpawn()
    {
        var position = spawns[UnityEngine.Random.Range(0,2)].transform.position;
        SpawnEnemy(enemy, position);
    }

    void SpawnEnemy(GameObject enemy, Vector3 position)
    {
        var enemyInstance = Instantiate(enemy, position, new Quaternion(), enemyContainer.transform);
        var enemyController = enemyInstance.GetComponent<EnemyController>();
        enemyController.target = target;
        enemyController.player = player;
        enemyController.whipController = whipController;
    }
}
