using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void UpdateHealthBar(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }
}
