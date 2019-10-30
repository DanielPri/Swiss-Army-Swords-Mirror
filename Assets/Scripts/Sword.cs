using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator swordAnimator;
    SwordType _swordType;

    public bool isAttacking;

    public enum SwordType
    {
        REGULAR, ICE, BRICK
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

        if (Input.GetMouseButtonDown(1))
        {
            SecondaryAttack();
        }

    }

    public virtual void PrimaryAttack()
    {
        IsAttacking = true;
        swordAnimator.SetTrigger("attack");
    }

    public virtual void SecondaryAttack()
    { }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
