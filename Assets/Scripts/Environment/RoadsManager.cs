using UnityEngine;

public class RoadsManager : MonoBehaviour
{
    public RoadSpawner roadSpawner;

    public void EndTriggerEnter()
    {
        roadSpawner.MoveRoad();
    }
}
