using UnityEngine;
using System.Collections.Generic;

public class AttackColliderController : MonoBehaviour
{
    public List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter (Collider other) 
    {
        if (!colliders.Contains(other))
            colliders.Add(other);
    }

    private void OnTriggerExit (Collider other) 
    {
        colliders.Remove(other);
    }

    void LateUpdate()
    {
        colliders.RemoveAll(c => c == null);
    }
}
