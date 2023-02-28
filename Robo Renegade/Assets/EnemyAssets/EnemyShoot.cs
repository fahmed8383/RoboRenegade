using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [Range(1, 10)]
    // 1 for med, 1 for boss
    [SerializeField] private float speed;

    [SerializeField] private GameObject bulletPrefab;
    private Transform firingPoint;
    private Transform target;


    [Range(0.1f, 20f)]
    // 6 for med, 0.3 for boss
    [SerializeField] private float fireRate;
    private float fireTimer;


    // Start is called before the first frame update
    void Start()
    {
        firingPoint = transform;
        // firingoint = new Vector2(transform.position.x, transform.position.y)

        // Debug.Log("firingPoint: " + firingPoint);

    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
    }
    private void Shoot()
    {
        target = GameObject.Find("Player").transform;


        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

        Instantiate(bulletPrefab, firingPoint.position, bulletRotation);

        //Debug.Log("bullet: " + target.position);
    }
}
