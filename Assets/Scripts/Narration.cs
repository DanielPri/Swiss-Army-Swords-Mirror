using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    AudioSource audioClip;
    [SerializeField] AudioSource previous;
    bool triggered = false;
    bool wait = false;

    void Start()
    {
        audioClip = GetComponent<AudioSource>();
        audioClip.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && !audioClip.isPlaying)
        {
            Destroy(gameObject, 10f);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && !triggered)
        {
            if (previous != null)
            {
                Debug.Log("YOU'RE IN MY NARRATION LAYER");
                if (previous.isPlaying)
                {
                    Debug.Log("waiting");
                    wait = true;
                }
                else
                {
                    Debug.Log("Done waiting");
                    wait = false;
                }
            }
            if (!wait)
            {
                triggered = true;
                audioClip.Play();
            }
        }
    }
}
