using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2D : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    // Gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;


    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
     

    private Rigidbody2D rb;
    private float mx;
    private float my;   

    private float fireTimer;

    private Vector2 cursorPos;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(cursorPos.y - transform.position.y, cursorPos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
    }

    // private void FixedUpdate()
    // {
    //     rb.velocity = new Vector2(mx, my).normalized * speed;
    // }

    private void Shoot()
    {
        // GameObject bullet = 
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // rb.AddForce(firingPoint.up * speed, ForceMode2D.Impulse);
    }
}
