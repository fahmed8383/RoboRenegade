using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Transform playerTransform;
    
    private static int maxHealth = 70;
    private static int health;
    private static bool invincible = false;

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
        if(!invincible)
        {
            health = Mathf.Max(0, health - damage);
            healthBar.SetHealth(health);
            FindObjectOfType<AudioManager>().Play("Damage");
        }
    }

    public void Heal(int hpGain)
    {
        FindObjectOfType<AudioManager>().Play("Heal");
        health = Mathf.Min(maxHealth, health + hpGain);
        healthBar.SetHealth(health);
    }

    private void Awake()
    {
        instance = this;   
    }
    
    public void SetInvincible(bool inv)
    {
        invincible = inv;
    }
}
