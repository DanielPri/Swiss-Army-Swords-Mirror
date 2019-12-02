using UnityEngine;
using System.Collections;

public class LavaDemonTrigger : MonoBehaviour
{
    private LavaDemon lavaDemon;
    private bool onlyOnce;

    private void Start()
    {
        lavaDemon = GameObject.FindGameObjectWithTag("LavaDemon").GetComponent<LavaDemon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !onlyOnce)
        {
            lavaDemon.GetComponent<Animator>().SetBool("isSpawn", true);
            onlyOnce = true;
        }
    }
}
