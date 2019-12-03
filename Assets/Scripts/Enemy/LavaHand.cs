using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHand : MonoBehaviour
{
    protected Player player;

    private bool once = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && once)
        {
            player.playerHPBar.DecreaseHitpoint(3);
            once = false;
        }
    }

}
