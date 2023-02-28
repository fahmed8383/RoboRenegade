using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameState gs;

    private Vector2 movement;
    private bool dodging = false;
    private bool dodgeValid = true;
    private bool timeStopValid = true;

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
                StartCoroutine(StopTime());
                StartCoroutine(StopTimeCooldown(cooldown));
            }
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
        Time.timeScale = 0.1f;
        animator.speed = 10f;
        moveSpeed = moveSpeed*10;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
        animator.speed = 1f;
        moveSpeed = moveSpeed / 10;
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
