using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electroshock : Enemy
{
    GameObject player;
    HitpointBar playerHPBar;

    new void Start()
    {
        player = GameObject.Find("Player");
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
    }

    new void Update()
    {
        Vector2 target = player.transform.position - transform.position;
        transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerHPBar.DecreaseHitpoint(3);
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerHPBar.DecreaseHitpoint(3);
        }
    }
}
