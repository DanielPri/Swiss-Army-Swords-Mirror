using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorHandler : MonoBehaviour
{
    [SerializeField]
    Door door;
    AudioSource audioSource;

    GameObject boss;

    bool bossSpawned = false;
    bool isDoorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfBossSpawned();
        checkIfBossDied();
    }

    private void checkIfBossDied()
    {
        if (bossSpawned)
        {
            Debug.Log("Boss Spawned!!");
            if(boss == null && !isDoorOpen)
            {
                door.toggle();
                isDoorOpen = true;
                audioSource.Play();
            }
        }
    }

    private void checkIfBossSpawned()
    {
        if (!bossSpawned)
        {
            boss = GameObject.Find("Ghost(Clone)");
            if(boss != null)
            {
                bossSpawned = true;
            }
            
        }
    }
}
