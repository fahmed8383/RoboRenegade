using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public float maxHP;
    public float atk;
    public float atkInterval;
    public float moveSpeed;
    public float freezeDuration;
    public float dropRate; // float value between 0 and 1
    public GameObject expItem;

    public AudioSource deathSound;
    public Rigidbody2D rb;
    public Collider2D collBody, collArea;
    public SpriteRenderer rend;
    public Animator anim;

    private float health;
    private Vector2 moveDirection;
    private bool alive = true;
    private bool frozen = false;
    private Transform target;
    private float timer = 0, freezeTimer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHP;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //FreezeEnemy();
            //Debug.Log("space pressed");
            //TakeDamage(10);
        }

        SetDirection();

        if (health <= 0 && alive)
        {
            alive = false;
            Kill();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // do damage at regular intervals

        if (collision.attachedRigidbody && collision.attachedRigidbody.gameObject.name == "Player")
        {
            //TakeDamage(10);
            if (timer == 0)
            {
                // deal damage equal to its atk
                //Debug.Log("Damage");
                timer += Time.deltaTime;
            }
            else if(timer >= atkInterval)
            {
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private IEnumerator FlashDamageColor()
    {
        rend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // enemy possibly takes damage
        if (collision.gameObject.CompareTag("PlayerBullet")){
            TakeDamage(10);
            StartCoroutine(FlashDamageColor());
        }

        // if evolved laser collides with enemy, set frozen = true
        //frozen = true;
    }

    void SetDirection()
    {
        if (frozen)
        {
            moveDirection = new Vector2(0, 0);
            if (freezeTimer >= freezeDuration)
            {
                frozen = false;
                freezeTimer = 0;
                anim.enabled = true; // unfreeze enemy
            }
            else
            {
                freezeTimer += Time.deltaTime;
            }
        }
        else
        {
            moveDirection = (target.position - transform.position).normalized;
            if (moveDirection != Vector2.zero)
            {
                anim.SetFloat("HorizontalDirection", moveDirection.x);
            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void FreezeEnemy()
    {
        frozen = true;
        anim.enabled = false;
    }

    void Kill()
    {
        deathSound.Play();
        // insert death animation and sound effect
        collBody.enabled = false;
        collArea.enabled = false;
        rend.enabled = false;

        DropEXP();
        Destroy(gameObject, deathSound.clip.length);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    void DropEXP()
    {
        if(Random.value <= dropRate)
        {
            // drop exp item
            Instantiate(expItem, transform.position, transform.rotation);
            Debug.Log("exp drop");
        }
    }
}
