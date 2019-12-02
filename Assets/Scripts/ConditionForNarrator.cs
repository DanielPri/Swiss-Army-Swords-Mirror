using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionForNarrator : MonoBehaviour
{
    [SerializeField]
    bool triggerCondition = true;

    [SerializeField]
    bool destroyCondition = false;

    [SerializeField]
    GameObject observedObject;

    public bool conditionMet = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(triggerCondition && col.tag == "Player")
        {
            Debug.Log("Condition met!");
            conditionMet = true;
        }
    }

    void Update()
    {
        if(destroyCondition && observedObject == null)
        {
            conditionMet = true;
        }
    }
    
}
