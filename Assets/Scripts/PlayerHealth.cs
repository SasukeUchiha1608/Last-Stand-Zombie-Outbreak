using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerHUD hud;
    public int maxHealth = 3;
    public int currentHealth;

    public float regenRate = 1f;     // How often to regen (every X seconds)
    public float regenDelay = 30f;    // How long after taking damage before regen starts

    private float lastHitTime;       // Last time player was hit
    private float lastRegenTime;     // Last time player regenerated
    private PlayerDamageFlash damageFlash;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        hud = FindObjectOfType<PlayerHUD>();
        hud?.UpdateHealth(currentHealth, maxHealth);
        damageFlash = FindObjectOfType<PlayerDamageFlash>();
    }

    void Update()
    {
        // Regenerate health if enough time has passed after damage
        if (!isDead && currentHealth < maxHealth && Time.time > lastHitTime + regenDelay)
        {
            if (Time.time >= lastRegenTime + regenRate)
            {
                currentHealth += 1;
                hud?.UpdateHealth(currentHealth, maxHealth);
                currentHealth = Mathf.Min(currentHealth, maxHealth);

                lastRegenTime = Time.time;
                Debug.Log("Player regenerated 1 health. Current HP: " + currentHealth);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        damageFlash?.TriggerFlash();
        currentHealth -= damage;
        hud?.UpdateHealth(currentHealth, maxHealth);
        currentHealth = Mathf.Max(0, currentHealth);
        lastHitTime = Time.time;

        Debug.Log("Player took damage! Current HP: " + currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");
        GameOverManager.Instance.ShowGameOver(); // if using singleton
    }
}
