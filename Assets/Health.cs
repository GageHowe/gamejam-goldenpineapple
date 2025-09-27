using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

public int GetHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        // For now just destroy the game object
        Destroy(gameObject);
    }
}
