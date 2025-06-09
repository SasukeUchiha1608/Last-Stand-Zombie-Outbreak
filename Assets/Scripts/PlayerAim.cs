using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 5f; // Adjust for smoother/faster rotation

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        AimAtMouse();
    }

    void AimAtMouse()
    {
        // Get mouse position in world coordinates
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Calculate the rotation angle (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation (adjust for your sprite's default orientation)
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(0, 0, angle),
            rotationSpeed * Time.deltaTime
        );
    }
}