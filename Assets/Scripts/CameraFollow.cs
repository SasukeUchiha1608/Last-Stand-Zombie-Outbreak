using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target; // Drag your Player object here in Inspector
    public float smoothSpeed = 0.125f; // Lower = smoother, Higher = snappier
    public Vector3 offset = new Vector3(0, 0, -10); // Adjust Z for 2D

    void LateUpdate()
    {
        if (target == null) return; // Safety check

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}