using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Empty GameObject marking where bullets spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f; // Delay between shots

    [Header("Audio")]
    public AudioClip shootSound; // Assign your sound effect in the Inspector
    private AudioSource _audioSource;

    private float _nextFireTime;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.volume = 0.8f; // Default volume
            _audioSource.spatialBlend = 0f; // Force 2D sound
        }

        if (firePoint == null)
        {
            Debug.LogWarning("FirePoint not assigned! Defaulting to player position.");
            firePoint = transform; // Fallback to player's position
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime) // Left-click
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Play shoot sound with null checks
        if (shootSound != null && _audioSource != null)
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(shootSound);
        }

        // Instantiate bullet if prefab exists
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                // Calculate direction (mouse to player)
                Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - (Vector2)firePoint.position).normalized;

                // Fire bullet
                rb.velocity = direction * bulletSpeed;
            }

            // Auto-destroy bullet after time
            Destroy(bullet, 2f);
        }
        else
        {
            if (bulletPrefab == null) Debug.LogError("Bullet prefab not assigned!");
            if (firePoint == null) Debug.LogError("Fire point not assigned!");
        }
    }
}