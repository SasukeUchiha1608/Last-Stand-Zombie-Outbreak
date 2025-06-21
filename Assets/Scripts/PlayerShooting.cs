using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    private PlayerHUD hud;

    [Header("Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Empty GameObject marking where bullets spawn
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f; // Delay between shots

    [Header("Audio")]
    public AudioClip shootSound; // Assign your sound effect in the Inspector
    private AudioSource _audioSource;

    [Header("Ammo Settings")]
    public int magazineSize = 10;
    public float reloadTime = 2f;
    public bool isReloading = false;
    private int currentAmmo;

    public ParticleSystem muzzleFlash; // Assign in Inspector
    private float _nextFireTime;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;

        currentAmmo = magazineSize;

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

        hud = FindObjectOfType<PlayerHUD>();
        hud?.UpdateAmmo(currentAmmo, magazineSize);

    }

    void Update()
    {
        // Don't allow input when paused or game is over
        if (Time.timeScale == 0f || isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                _nextFireTime = Time.time + fireRate;
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }



    void Shoot()
    {

        currentAmmo--;
        hud?.UpdateAmmo(currentAmmo, magazineSize);

        // Play muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogError("MuzzleFlash reference is null!");
        }

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
                // Fire bullet
                rb.velocity = firePoint.right * bulletSpeed;
            }

            // Auto-destroy bullet after time
            Destroy(bullet, 5f);
        }
        else
        {
            if (bulletPrefab == null) Debug.LogError("Bullet prefab not assigned!");
            if (firePoint == null) Debug.LogError("Fire point not assigned!");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        hud?.SetReloading(true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;

        hud?.UpdateAmmo(currentAmmo, magazineSize);
        hud?.SetReloading(false);



        // TODO: Hide reloading icon here
    }

}