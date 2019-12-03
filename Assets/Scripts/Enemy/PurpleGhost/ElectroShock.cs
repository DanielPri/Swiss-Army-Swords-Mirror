using UnityEngine;
using System.Collections;

public class ElectroShock : MonoBehaviour
{
    private Player player;
    private bool once;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && once)
        {
            player.playerHPBar.DecreaseHitpoint(1);
            once = false;
        }
    }


}
