using System;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class HouseColorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer spriteShadow;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance = 300f;
    [SerializeField] private float fadeIntencity = 1f;
    [SerializeField] private float fadePower = 2f;
    [SerializeField] private float fadeMaximum = 1f;
    [SerializeField] private float cellShading = 10f;

    void Update()
    {
        if (sprite != null && spriteShadow != null && cameraTransform != null)
        {
            var distance = math.abs(transform.position.z - cameraTransform.position.z) / maxDistance;
            var intencity = math.clamp(math.pow(distance * fadeIntencity, fadePower), 0, fadeMaximum);
            intencity = (math.round(intencity * cellShading)) / cellShading;

            var newColor = sprite.color;
            newColor.a = math.lerp(0, 1, intencity);
            sprite.color = newColor;
            spriteShadow.color = newColor;
            spriteShadow.sortingOrder = (int)(cameraTransform.position.z - transform.position.z) - 500;
        }
        else
        {
            Debug.LogWarning("Sprite or camera missing for " + gameObject.name);
        }
    }
}
