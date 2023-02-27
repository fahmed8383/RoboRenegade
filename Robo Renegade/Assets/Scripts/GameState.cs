using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    private static int maxHealth = 70;
    private static int health;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    public void Heal(int hpGain)
    {
        FindObjectOfType<AudioManager>().Play("Heal");
        health = Mathf.Min(maxHealth, health + hpGain);
        healthBar.SetHealth(health);
    }

}
