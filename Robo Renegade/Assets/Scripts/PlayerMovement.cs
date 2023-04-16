using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public static float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameState gs;
    public GameObject spawner;
    public SpriteRenderer shield;

    private Vector2 movement;
    private bool dodging = false;
    private bool dodgeValid = true;
    private bool timeStopValid = true;
    [SerializeField] TextMeshProUGUI cooldownText;

    Level level;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (dodgeValid && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dodgeValid = false;
            dodging = true;
            gs.SetInvincible(true);
            animator.SetBool("Dodge", true);
            FindObjectOfType<AudioManager>().Play("Dodge");
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * 20);
            Invoke("EndDodge", 0.2f);
            Invoke("ResetDodgeTimer", 2);
        }

        if (Input.GetKeyDown(KeyCode.Q) && timeStopValid)
        {
            int cooldown = 11 - Level.getActiveLevel();
            if (cooldown != 11)
            {
                cooldown = Mathf.Max(5, cooldown);
                StartCoroutine(StopTime());
                StartCoroutine(StopTimeCooldown(cooldown));
            }
        }
        if (Level.getActiveLevel() >= 1 && timeStopValid) {
            cooldownText.text = "Q: READY";
        } else if (Level.getActiveLevel() >= 1 && !timeStopValid)
        {
            cooldownText.text = "Q: On Cooldown";
        }

        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     Debug.Log("pressed P");
        //     level = GetComponent<Level>();
        //     level.AddExperience(1000);
        //     Debug.Log("called level");
        // }

        movement = Vector2.ClampMagnitude(movement, 1);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (!dodging)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void EndDodge()
    {
        dodging = false;
        gs.SetInvincible(false);
        animator.SetBool("Dodge", false);
    }

    private void ResetDodgeTimer()
    {
        dodgeValid = true;
    }

    private IEnumerator StopTime()
    {
        spawner.GetComponent<SpawnerScript>().canSpawn = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<enemyScript>().moveSpeed = 0;
            enemy.GetComponent<enemyScript>().anim.speed = 0;
            EnemyShoot enemyShoot;
            if(enemy.TryGetComponent<EnemyShoot>(out enemyShoot))
            {
                enemyShoot.canShoot = false;
            }
        }

        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject bullet in playerBullets)
        {
            bullet.GetComponent<PlayerBullet>().speed = 0;
        }

        //GameObject.Find("FiringPoint").GetComponent<Gun2D>().canShoot = false;

        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            bullet.GetComponent<EnemyBullet>().speed = 0;
        }

        GameState.invincible = true;
        shieldScript.toggleLockInvDisable();
        yield return new WaitForSeconds(3f);
        shieldScript.toggleLockInvDisable();
        if (!shield.enabled || Level.shieldLevel == 0)
        {
            GameState.invincible = false;
        }

        spawner.GetComponent<SpawnerScript>().canSpawn = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<enemyScript>().moveSpeed = 1f;
                enemy.GetComponent<enemyScript>().anim.speed = 1f;
                EnemyShoot enemyShoot;
                if (enemy.TryGetComponent<EnemyShoot>(out enemyShoot))
                {
                    enemyShoot.canShoot = true;
                }
            }
        }
        foreach (GameObject bullet in playerBullets)
        {
            if (bullet != null)
            {
                bullet.GetComponent<PlayerBullet>().speed = 10f;
            }
        }
        GameObject.Find("FiringPoint").GetComponent<Gun2D>().canShoot = true;
        foreach (GameObject bullet in enemyBullets)
        {
            if (bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().speed = 10f;
            }
        }
    }

    private IEnumerator StopTimeCooldown(int cooldown)
    {
        timeStopValid = false;
        yield return new WaitForSeconds(cooldown);
        timeStopValid = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            gs.TakeDamage(1);
        }
        // if (collision.gameObject.CompareTag("PlayerBullet")){
        //     Debug.Log("BulletCollision");
        //     Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // }
    }

}
