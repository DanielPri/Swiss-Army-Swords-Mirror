using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHand : MonoBehaviour
{
    protected Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player.playerHPBar.DecreaseHitpoint(1);
        }
    }
}
