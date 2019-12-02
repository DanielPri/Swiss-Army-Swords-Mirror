using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSword : Sword
{
    public ParticleSystem particleFire = null;
    float attackTimeElapsed;
    bool charging = false;
    [SerializeField] float chargeDuration;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.FIRE;
        particleFire = GetComponentsInChildren<ParticleSystem>()[0];
    }

    public override void Update()
    {
        base.Update();
        swordAnimator.SetBool("charging", charging);
        if (Input.GetButtonDown("Fire2"))
        {
            attackTimeElapsed = 0;
            particleFire.Play();
            charging = true;
        }
        if (charging && Input.GetButton("Fire2"))
        {
            timeCharge();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            charging = false;
            particleFire.Stop();
        }
    }

    public override void Ability()
    {
        base.Ability();
    }
    private void timeCharge()
    {
        if (attackTimeElapsed < chargeDuration)
        {
            attackTimeElapsed += Time.deltaTime;
        }
    }
}
