using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 1f;
    public float damageCooldown = 1f; // 1 second between damage instances
    private float _lastDamageTime;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= _lastDamageTime + damageCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage((int)damage);
                    _lastDamageTime = Time.time;
                }
            }
        }
    }
}
