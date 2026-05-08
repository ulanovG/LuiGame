using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spGroup", menuName = "Scriptable Objects/spGroup")]
public class spGroup : ScriptableObject, IspGroup
{
    public List<spMobSpawn> mobSpawns;

    public List<spMobSpawn> MobSpawns
    {
        get
        {
            return mobSpawns;
        }
    }
}
