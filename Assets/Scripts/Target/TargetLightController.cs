using UnityEngine;
using System;

public class TargetLightController : MonoBehaviour
{
    public static event Action onGameLose;

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("LOSE");
        onGameLose?.Invoke();
    }
}
