using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spRandomGroup", menuName = "Scriptable Objects/spRandomGroup")]
public class spRandomGroup : ScriptableObject, IspGroup
{
    public List<spGroup> groups;

    public List<spMobSpawn> MobSpawns
    {
        get
        {
            return groups[Random.Range(0,groups.Count)].MobSpawns;
        }
    }

}
