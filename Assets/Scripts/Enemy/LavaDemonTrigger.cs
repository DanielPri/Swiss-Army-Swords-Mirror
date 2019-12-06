using UnityEngine;
using System.Collections;

public class LavaDemonTrigger : MonoBehaviour
{
    private LavaDemon lavaDemon;
    private bool onlyOnce;
    AudioSource audioManager;
    AudioSource audioSource;

    private void Start()
    {
        lavaDemon = GameObject.FindGameObjectWithTag("LavaDemon").GetComponent<LavaDemon>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !onlyOnce)
        {
            lavaDemon.GetComponent<Animator>().SetBool("isSpawn", true);
            lavaDemon.fightStart = true;
            audioManager.Stop();
            audioSource.Play();
            onlyOnce = true;
        }
    }
}
