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
        Attack();
        Ability();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
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

    private void Ability()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            swordAnimator.SetTrigger("ability");
        }
    }
}
