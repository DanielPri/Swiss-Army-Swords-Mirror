using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinalBossLifeBarSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossLifeBar;
    GameObject theBar;
    Player player;
    bool onlyOnce;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        onlyOnce = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")        
        {
            if (!player.dialogueActive && !onlyOnce)
            {
                theBar = Instantiate(bossLifeBar, new Vector2(0, -200), Quaternion.identity);
                theBar.transform.SetParent(GameObject.Find("UI Canvas").transform, false);
                onlyOnce = true;
            }
        }
    }

}