using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] int damageDealt = 1;
    // How quickly enemy takes damage
    float damageDelay;
    Animator swordAnimator;
    SwordType _swordType;

    public bool damaging;

    public enum SwordType
    {
        REGULAR, ICE, BRICK
    }

    public SwordType swordType { get { return _swordType; } set { _swordType = value; } }

    public virtual void Start()
    {
        damageDelay = 0.02f * damageDealt;
        damaging = false;
        swordAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        Ability();
    }

    public virtual void Attack()
    {
        if (Input.GetButtonDown("Fire1") && damaging == false)
        {
            damaging = true;
            swordAnimator.SetTrigger("attack");
            damageDelay = 0.02f * damageDealt;
        }

        if (damaging)
        {
            damageDelay -= Time.deltaTime;

            if (damageDelay <= 0f)
            {
                damaging = false;
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
