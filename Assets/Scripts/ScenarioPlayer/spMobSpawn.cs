using UnityEngine;

[CreateAssetMenu(fileName = "spMobSpawn", menuName = "Scriptable Objects/spMobSpawn")]
public class spMobSpawn : ScriptableObject
{
    public GameObject enemy;
    public string spawnName;
    public float movementCenterBias = 0f;
}
