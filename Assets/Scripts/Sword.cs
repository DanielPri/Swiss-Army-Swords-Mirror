using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator swordAnimator;

    void Start()
    {
        swordAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swordAnimator.SetTrigger("attack");
        }
    }
}
