using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float attackDelay;
    Animator swordAnimator;
    SwordType _swordType;

    public bool attacking;

    public enum SwordType
    {
        REGULAR, ICE, BRICK
    }

    public SwordType swordType { get { return _swordType; } set { _swordType = value; } }

    public virtual void Start()
    {
        attackDelay = 0.33f;
        attacking = false;
        swordAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        Ability();
    }

    public virtual void Attack()
    {
        if (Input.GetButtonDown("Fire1") && attacking == false)
        {
            attacking = true;
            swordAnimator.SetTrigger("attack");
            attackDelay = 0.33f;
        }

        if (attacking)
        {
            attackDelay -= Time.deltaTime;

            if (attackDelay <= 0f)
            {
                attacking = false;
            }
        }
    }

    public virtual void Ability()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            swordAnimator.SetTrigger("ability");
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
