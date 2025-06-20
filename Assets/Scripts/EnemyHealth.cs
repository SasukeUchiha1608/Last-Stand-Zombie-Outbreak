using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int _currentHealth;

    [SerializeField] private GameObject deadZombiePrefab; // Assign in Inspector

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deadZombiePrefab != null)
        {
            Instantiate(deadZombiePrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Dead zombie prefab not assigned on: " + gameObject.name);
        }

        Destroy(gameObject);
    }

}
