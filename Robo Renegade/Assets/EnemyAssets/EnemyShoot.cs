using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private float speed = 2f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    private Transform target;


    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 5f;
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

        // Vector2 directionToTarget = target.position - transform.position;
    
        // float angle = Vector3.Angle(Vector3.right, directionToTarget);
        // if(target.position.y < firingPoint.position.y) angle *= -1;
        // Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);


        // float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        // transform.localRotation = Quaternion.Euler(0, 0, angle);
        // Vector3 dirToPlayer = (player.transform.position - enemy.transform.position);


        Instantiate(bulletPrefab, firingPoint.position, bulletRotation);

        Debug.Log("bullet: " + target.position);
    }
}
