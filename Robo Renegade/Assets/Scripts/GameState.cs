using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    private static int maxHealth = 10;
    private static int health = 10;
    private static bool invincible = false;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    public void TakeDamage(int damage)
    {
        if(!invincible)
        {
            health -= damage;
            healthBar.SetHealth(health);
            FindObjectOfType<AudioManager>().Play("Damage");
        }
    }

    public void Heal()
    {
        health = Mathf.Min(10, health + 2);
        healthBar.SetHealth(health);
    }

    public void SetInvincible(bool inv)
    {
        invincible = inv;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "health_pack(Clone)")
        {
            Heal();
            FindObjectOfType<AudioManager>().Play("Heal");
            Destroy(collision.gameObject);
        }
    }
}
