using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth player;

    void Awake()
    {
        slider.value = player.maxHealth;
    }

    public void UpdateHealthUI(int health)
    {
        slider.value = player.CurrentHealth();
    }
}
