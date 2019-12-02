using UnityEngine;
using System.Collections;

public class SwitchMusicTrigger : MonoBehaviour
{
    public AudioClip newTrack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (newTrack != null)
            AudioManager.Instance.ChangeBGM(newTrack);
        }
    }
}
