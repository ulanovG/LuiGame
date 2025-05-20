using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private float position;
    public Camera mainCamera;
    public float borders;
    public Transform followTarget;
    public float targetShift;

    void Update()
    {
        position = mainCamera.ScreenToViewportPoint(Input.mousePosition).x;

        if (position <= borders)
        {
            var pos = followTarget.localPosition;
            pos.x = ((borders - position) / borders) * -targetShift;
            followTarget.localPosition = pos;
        }
        else if (position >= (1 - borders))
        {
            var pos = followTarget.localPosition;
            pos.x = ((borders - (1 - position)) / borders) * targetShift;
            followTarget.localPosition = pos;
        }
        else if (followTarget.position.x != 0)
        {
            var pos = followTarget.localPosition;
            pos.x = 0f;
            followTarget.localPosition = pos;
        }

    }
}
