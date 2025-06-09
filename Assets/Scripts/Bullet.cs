using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for enemy collision first
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
            return; // Exit early after hitting an enemy
        }

        // Check for wall collision
        TilemapCollider2D wallCollider = other.GetComponent<TilemapCollider2D>();
        if (wallCollider != null && wallCollider.gameObject.name == "Walls")
        {
            Destroy(gameObject);
        }
    }
}