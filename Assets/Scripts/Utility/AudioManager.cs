using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM;

    public static AudioManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ChangeBGM(AudioClip newBGM)
    {
        if (BGM.name == newBGM.name) return;

        BGM.Stop();
        BGM.clip = newBGM;
        BGM.Play();
    }
}
