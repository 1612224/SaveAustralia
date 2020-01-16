using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider.value = PlayerHealth.maxHealth;
    }

    public void UpdateHealthUI(int health)
    {
        slider.value = health;
    }
}
