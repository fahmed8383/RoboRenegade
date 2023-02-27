using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanobotScript : MonoBehaviour
{
    public float dmgInterval;
    public int dmg;
    private float timer = 0;
    public float range;
   
    void FixedUpdate()
    {
        if (timer == 0)
        {
            //Debug.Log("Damage");
            damageInArea();
            timer += Time.deltaTime;
        }
        else if (timer >= dmgInterval)
        {
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void damageInArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D collision in colliders)
        {
            if (collision.attachedRigidbody && collision.attachedRigidbody.gameObject.tag == "Enemy")
            {
                collision.attachedRigidbody.gameObject.GetComponent<enemyScript>().TakeDamage(dmg);

            }
        }
    }
}
