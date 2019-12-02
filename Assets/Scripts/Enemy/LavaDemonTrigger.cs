using UnityEngine;
using System.Collections;

public class LavaDemonTrigger : MonoBehaviour
{
    private Animator lavaDemon;

    private void Start()
    {
        lavaDemon.SetBool("isSpawning", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {

        }
    }
}
