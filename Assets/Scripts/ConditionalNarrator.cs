using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalNarrator : MonoBehaviour
{

    List<ConditionForNarrator> conditions = new List<ConditionForNarrator>();
    AudioSource audioClip;

    [SerializeField]
    AudioSource previous;

    bool canPlay = false;
    bool triggered = false;
    bool wait = false;

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();
        audioClip.playOnAwake = false;
        foreach (Transform child in transform)
        {
            conditions.Add(child.gameObject.GetComponent<ConditionForNarrator>());
        }
        Debug.Log("conditions size: " + conditions.Count);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].conditionMet)
            {
                canPlay = false;
                break;
            }
            else if(i == conditions.Count-1)
            {
                Debug.Log("canPlay");
                canPlay = true;
            }
        }

        if (canPlay && !triggered)
        {
            Debug.Log("CanPlay for real!");
            if (previous != null)
            {
                Debug.Log("YOU'RE IN MY CONDITIONAL NARRATION LAYER");
                if (previous.isPlaying)
                {
                    wait = true;
                }
                else
                {
                    wait = false;
                }
            }
            if (!wait)
            {
                triggered = true;
                audioClip.Play();
            }
        }
        if (triggered && !audioClip.isPlaying)
        {
            Destroy(gameObject, 10f);
        }
    }
}
