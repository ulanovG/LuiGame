using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spScenario", menuName = "Scriptable Objects/spScenario")]
public class spScenario : ScriptableObject
{
    public List<spWave> waves;
}
