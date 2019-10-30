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
        Ability();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            swordAnimator.SetTrigger("attack");
        }
    }

    private void Ability()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            swordAnimator.SetTrigger("ability");
        }
    }
}
