using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] int damageDealt = 1;
    // How quickly enemy takes damage
    float damageDelay;
    Animator swordAnimator;
    Animator playerAnimator;
    SwordType _swordType;
    protected Player player;

    public bool damaging;

    bool isPlayerIdle = true;

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
        swordAnimator.SetBool("playerIdle", isPlayerIdle);
        playerAnimator = GetComponentsInParent<Animator>()[1]; // sword is at index 0, player at 1
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (playerAnimator.GetBool("isMoving"))
        {
            isPlayerIdle = false;
        } else
        {
            isPlayerIdle = true;
        }

        if (Input.GetButtonDown("Fire1") && damaging == false)
        {
            Attack();
        }

        if (damaging)
        {
            damageDelay -= Time.deltaTime;

            if (damageDelay <= 0f)
            {
                damaging = false;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Ability();
        }
    }

    public virtual void Attack()
    {
        damaging = true;
        swordAnimator.SetTrigger("attack");
        damageDelay = 0.02f * damageDealt;
    }

    public virtual void Ability()
    {
        swordAnimator.SetTrigger("ability");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
