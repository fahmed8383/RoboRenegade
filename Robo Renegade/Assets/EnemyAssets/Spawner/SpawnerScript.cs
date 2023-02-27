using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    /// <summary>
    /// to be attached to pllayer as a child, player being at the center of the radius
    /// </summary>

    public float spawnInterval;
    public float radius;
    public float mediumDiffChance = 0.5f; // chance of spawning meduim enemies in medium difficulty
    private float timer = 0;
    private int difficulty = 0;

    public GameObject enemyEasy;
    public GameObject enemyMedium;
    public GameObject enemyBoss;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnEnemy();
            timer = 0;
        }
    }

    void spawnEnemy()
    {
        // determine difficulty based on game duration
        // before 2.5 mins
        //spawnEasy(getSpawnPosition());

        // after 2.5 min, before 5 min
        spawnMedium(getSpawnPosition());

        // after 5 min
        //spawnHard(getSpawnPosition());
    }

    Vector3 getSpawnPosition()
    {
        float angle = Random.value * 2 * Mathf.PI;
        Vector3 spawnPosition = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
        return spawnPosition;
    }

    void spawnEasy(Vector3 spawnPos) // only easy enemies
    {
        Instantiate(enemyEasy, spawnPos, transform.rotation);
    }

    void spawnMedium(Vector3 spawnPos) // 50% chance of easy or medium enemies
    {
        if (Random.value <= mediumDiffChance)
        {
            Instantiate(enemyMedium, spawnPos, transform.rotation);
        }
        else
        {
            Instantiate(enemyEasy, spawnPos, transform.rotation);
        }
        
    }

    void spawnHard(Vector3 spawnPos) // only medium enemies
    {
        Instantiate(enemyMedium, spawnPos, transform.rotation);
    }

    // to be called at 5 min of game duration
    void spawnBoss()
    {
        Instantiate(enemyBoss, getSpawnPosition(), transform.rotation);
    }
}
