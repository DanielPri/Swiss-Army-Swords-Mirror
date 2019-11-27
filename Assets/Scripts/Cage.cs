using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    Animator cageAnimator;

    void Start()
    {
        cageAnimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            cageAnimator.SetTrigger("Destroy");
            gameObject.layer = LayerMask.NameToLayer("Background");
            foreach(Transform child in transform)
            {
                child.parent = null;
            }
            Destroy(gameObject, 3);
        }
    }
}
