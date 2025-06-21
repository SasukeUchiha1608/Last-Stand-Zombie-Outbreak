using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 5f; // Adjust for smoother/faster rotation
    public Transform graphicsTransform;

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
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)graphicsTransform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        graphicsTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}