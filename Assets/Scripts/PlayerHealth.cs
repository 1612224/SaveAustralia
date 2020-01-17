using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    int currentHealth;
    public static int maxHealth = 100;

    [Header("Health Events")]
    public IntEvent damageEvent;
    public IntEvent healEvent;
    public UnityEvent deadEvent;

    public int CurrentHealth()
    {
        return currentHealth;
    }

    void Awake()
    {
        currentHealth = maxHealth;

        if (damageEvent == null)
        {
            damageEvent = new IntEvent();
        }
        if (healEvent == null)
        {
            healEvent = new IntEvent();
        }
        if (deadEvent == null)
        {
            deadEvent = new UnityEvent();
        }
    }

    public void Damage(int damage)
    {
        if (damage < 0)
        {
            throw new System.Exception("Damage can't be negative");
        }

        currentHealth = currentHealth < damage ? 0 : currentHealth - damage;
        damageEvent.Invoke(currentHealth);
        if (currentHealth == 0)
        {
            deadEvent.Invoke();
            GameObject obj = new GameObject();
            GameOverIndex component = obj.AddComponent<GameOverIndex>();
            obj.tag = "GameOverIndex";
            component.index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(5);
            Debug.Log("DEAD");
        }
    }
}
