using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldScript : MonoBehaviour
{

    public float upgrade_duration;
    public float duration;
    public float cooldown;
    private float timer;
    public SpriteRenderer sprite;
    private static bool invdisableLock;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        sprite.color = new Color(1f, 1f, 1f, .5f);
        invdisableLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.invincible)
        {
            if (timer < duration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (!invdisableLock)
                {
                    GameState.invincible = false;
                }
                sprite.enabled = false;
                timer = 0;
            }
        }
        else
        {
            if (timer < cooldown)
            {
                timer += Time.deltaTime;
            }
            else
            {
                GameState.invincible = true;
                sprite.enabled = true;
                timer = 0;
            }
        }
    }

    public static void toggleLockInvDisable()
    {
        invdisableLock = (!invdisableLock);
    }
}
