using UnityEngine;

public class RoadsManager : MonoBehaviour
{
    public RoadSpawner roadSpawner;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EndTriggerEnter()
    {
        roadSpawner.MoveRoad();
    }
}
