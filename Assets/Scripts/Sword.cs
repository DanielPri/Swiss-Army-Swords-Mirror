using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    AudioClip[] attacksSounds;
    [SerializeField] int damageDealt = 1;
    // How quickly enemy takes damage
    protected Animator swordAnimator;
    protected BoxCollider2D swordCollider;
    SwordType _swordType;

    [SerializeField]
    protected float damageDuration = 0.2f;
    protected float damageDelay;
    protected Player player;
    protected bool isAbilityUsed;
    public bool damaging;

    
    AudioSource audioSource;

    public enum SwordType
    {
        REGULAR, ICE, BRICK, LIGHT
    }

    public SwordType swordType { get { return _swordType; } set { _swordType = value; } }

    public virtual void Start()
    {
        damageDelay = damageDuration * damageDealt;
        damaging = false;
        swordAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        swordCollider = player.GetComponentInChildren<BoxCollider2D>();
        swordCollider.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && damaging == false)
        {
            Attack();
            swordCollider.enabled = true;
            makeAttackSound();
        }

        if (damaging)
        {
            damageDelay -= Time.deltaTime;

            if (damageDelay <= 0f)
            {
                damaging = false;
                swordCollider.enabled = false;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Ability();
        }
    }

    private void makeAttackSound()
    {
        audioSource.clip = attacksSounds[UnityEngine.Random.Range(0, attacksSounds.Length)];
        audioSource.time = 0.1f;
        audioSource.Play();
    }

    public virtual void Attack()
    {
        damaging = true;
        swordAnimator.SetTrigger("attack");
        damageDelay = damageDuration * damageDealt;
    }

    public virtual void Ability()
    {
        swordAnimator.SetTrigger("ability");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
