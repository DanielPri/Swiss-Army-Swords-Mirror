using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator swordAnimator;
    SwordType _swordType;
    bool isAttacking;

    public enum SwordType
    {
        REGULAR,
        ICE
    }

    public SwordType swordType { get { return _swordType; } set { _swordType = value; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    public virtual void Start()
    {
        swordAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrimaryAttack();
        }
        else
        {
            IsAttacking = false;
        }
    }

    public virtual void PrimaryAttack()
    {
        IsAttacking = true;
        swordAnimator.SetTrigger("attack");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
