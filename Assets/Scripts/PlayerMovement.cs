using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Read input here every frame
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate() {
        // Apply velocity in fixed time steps for smooth physics movement
        rb.velocity = movementInput * moveSpeed;
    }
}
