using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] public float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 20f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //   if (collision.gameObject.tag == "Player")
    //   {
    //         Debug.Log("Player");
    //         Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
    //   }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision", collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerCollision");

            // Physics.IgnoreCollision(collision.collider, collider);
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
            // collision.gameObject.GetComponent<TestEnemy>().TakeDamage(20f);
        // }
        Destroy(gameObject);
    }
}
