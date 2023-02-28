using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 2f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 5f;

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
            //Debug.Log("PlayerCollision");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            return;
        }
        Destroy(gameObject);
    }
}
