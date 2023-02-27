using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractItemScript : MonoBehaviour
{
    private Transform target;
    public Rigidbody2D rb;

    private float moveSpeed = 6;
    private bool targetFound = false;
    private Vector2 moveDirection = new Vector2(0, 0);
    public int PointsGain = 25; // generic point (EXP or HP)

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetFound)
        {
            moveDirection = (target.position - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // indicator to go towards player
        if (collision.attachedRigidbody && collision.attachedRigidbody.gameObject.name == "AttractorField")
        {
            //Debug.Log("found");
            targetFound = true;
        }
        else if (collision.attachedRigidbody && collision.attachedRigidbody.gameObject.name == "Player")
        {
            // Add EXP to player equal to EXPGain
            if (gameObject.tag == "EXP")
            {
                // add EXP to player
                Debug.Log("exp gained");
            }
            else if (gameObject.tag == "HP")
            {
                // add HP to player
                collision.attachedRigidbody.gameObject.GetComponent<GameState>().Heal(PointsGain);
                Debug.Log("hp gained");
            }
            Destroy(gameObject);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
