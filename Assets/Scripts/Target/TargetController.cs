using UnityEngine;

public class TargetController : MonoBehaviour
{
    public RoadsManager roadsManager;

    public float speed = 3f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        roadsManager.EndTriggerEnter();
    }
}
