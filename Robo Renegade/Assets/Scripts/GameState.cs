using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public Transform playerTransform;
    public SpriteRenderer sprite;
    public GameObject gameOver;
    
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
            StartCoroutine(FlashDamageColor());
            FindObjectOfType<AudioManager>().Play("Damage");
        }
        if(health == 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
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

    private IEnumerator FlashDamageColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
