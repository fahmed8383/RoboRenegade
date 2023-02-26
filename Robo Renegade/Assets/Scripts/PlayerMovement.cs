using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator animator;
    public GameState gs;

    private Vector2 movement;
    private bool dodging = false;
    private bool dodgeValid = true;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (dodgeValid && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dodgeValid = false;
            dodging = true;
            animator.SetBool("Dodge", true);
            FindObjectOfType<AudioManager>().Play("Dodge");
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime * 20);
            Invoke("EndDodge", 0.2f);
            Invoke("ResetDodgeTimer", 2);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FlashDamageColor());
            gs.TakeDamage(1);
            FindObjectOfType<AudioManager>().Play("Damage");
        }

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
        animator.SetBool("Dodge", false);
    }

    private void ResetDodgeTimer()
    {
        dodgeValid = true;
    }

    private IEnumerator FlashDamageColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
