using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    [SerializeField] private Slider slider;

    public float regenRate = 2.0f; // health points per second to regenerate

    public RandomSoundPlayer hurtSounds;

    public bool isPlayer = false;  // Flag to identify player

    public GameObject gameOverCanvas; // Assign only for player

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
        hurtSounds.Play();
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
        if (isPlayer)
        {
            // Show Game Over canvas and pause game for player
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            // Disable player controls here if needed
        }
        Destroy(gameObject);
    }
}
