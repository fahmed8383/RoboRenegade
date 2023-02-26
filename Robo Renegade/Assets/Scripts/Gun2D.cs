using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2D : MonoBehaviour
{
    // public GameObject player;

    [SerializeField] private float speed = 5f;

    // Gun variables
    [SerializeField] private GameObject bulletPrefab;
    // [SerializeField] private Transform firingPoint = this.transform;

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
        // rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        // cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // float angle = Mathf.Atan2(cursorPos.y - transform.position.y, cursorPos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        // transform.localRotation = Quaternion.Euler(0, 0, angle);

        // Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // diff.Normalize();

        // float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90f;

        // transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        // if (rotZ < -90 || rotZ > 90)
        // {
        //     if (player.transform.eulerAngles.y == 0)
        //     {
        //         transform.localRotation = Quaternion.Euler(180, 0, -rotZ);
        //     } else if(player.transform.eulerAngles.y == 180)
        //     {
        //         transform.localRotation = Quaternion.Euler(180, 180, -rotZ);
        //     }
        // }

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
        Transform firingPoint = this.transform;
        // GameObject bullet = 
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(firingPoint.rotation.eulerAngles.x, firingPoint.rotation.eulerAngles.y, firingPoint.rotation.eulerAngles.z - 90f));
        FindObjectOfType<AudioManager>().Play("BulletFire");

        // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // rb.AddForce(firingPoint.up * speed, ForceMode2D.Impulse);
    }
}
