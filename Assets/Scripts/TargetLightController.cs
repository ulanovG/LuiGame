using UnityEngine;

public class TargetLightController : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("LOSE");
    }
}
