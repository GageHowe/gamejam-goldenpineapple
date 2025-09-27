using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    [SerializeField] private Slider slider;

    public float regenRate = 0.05f; // health points per second to regenerate

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }

    void Update()
    {
        RegenerateHealth();
    }

    void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;  // add fractional amount directly
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
