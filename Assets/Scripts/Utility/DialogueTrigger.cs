using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool onlyOnce;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player") && !onlyOnce)
        {
            TriggerDialogue();
            onlyOnce = true;
        }
    }
}
