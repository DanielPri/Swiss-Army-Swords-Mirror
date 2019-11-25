using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCut : MonoBehaviour
{
    [SerializeField]
    HingeJoint2D ropeSegment;

    void Start()
    {
        GameObject.Find("Player");

    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sword")
        {
            Sword swordScript = col.GetComponent<Sword>();
            if (swordScript.damaging)
            {
                transform.parent.gameObject.GetComponent<HingeJoint2D>().enabled = false;
            }
        }
        
    }

}
