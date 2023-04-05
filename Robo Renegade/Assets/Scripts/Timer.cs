using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time = 0;
    public TextMeshProUGUI timeText;
    private GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        int diff = spawner.GetComponent<SpawnerScript>().difficulty;
        if (minutes == 5)
        {
            if (diff == 1)
            {
                spawner.GetComponent<SpawnerScript>().setHard();
                spawner.GetComponent<SpawnerScript>().spawnBoss();
                timeText.color = Color.red;
                Debug.Log("boss and hard diff");
            }
        }
        else if (minutes == 1)
        {
            if (diff == 0)
            {
                spawner.GetComponent<SpawnerScript>().setMedium();
                Debug.Log("med diff");
            }
        }
    }
}
