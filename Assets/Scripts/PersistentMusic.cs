using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    static PersistentMusic prefab;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (prefab == null)
        {
            prefab = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}