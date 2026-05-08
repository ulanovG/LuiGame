using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "spWave", menuName = "Scriptable Objects/spWave")]
public class spWave : ScriptableObject
{
    public List<ScriptableObject> groups;
    public float delay = 0.0f;
    public int repeat = 1;

    private void OnValidate() 
    {
        if(groups.Any(g => g != null && !(g is IspGroup)))
        {
            Debug.LogError($"Group does not implement IspGroup!");
        }
    }
}
