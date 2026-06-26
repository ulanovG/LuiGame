using UnityEngine;
using Unity.Mathematics;

public class ParticleFadeController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float maxDistance = 300f;
    [SerializeField] private float fadeIntencity = 1f;
    [SerializeField] private float fadePower = 2f;
    [SerializeField] private float fadeMaximum = 1f;
    [SerializeField] private float cellShading = 10f;

    void Update()
    {
        if (particle != null)
        {
            var distance = math.abs(transform.position.z - Camera.main.transform.position.z) / maxDistance;
            var intencity = math.clamp(math.pow(distance * fadeIntencity, fadePower), 0, fadeMaximum);
            intencity = (math.round(intencity * cellShading)) / cellShading;

            var main = particle.main;
            var gradient = main.startColor;
            var newColor = gradient.color;
            newColor.a = math.lerp(1, 0, intencity);
            gradient = newColor;
            main.startColor = gradient;
        }
        else
        {
            Debug.LogWarning("Sprite is missing for " + gameObject.name);
        }
    }
}
