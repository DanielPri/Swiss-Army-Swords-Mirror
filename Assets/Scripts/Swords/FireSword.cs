using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireSword : Sword
{
    public ParticleSystem particleFire = null;
    float attackTimeElapsed;
    bool charging = false;
    [SerializeField] float chargeDuration;

    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.FIRE;
        particleFire = GetComponentsInChildren<ParticleSystem>()[0];
        particleFire.Stop();
    }

    private void OnDisable()
    {
        charging = false;
        particleFire.Stop();
    }

    public override void Update()
    {
        base.Update();
        swordAnimator.SetBool("charging", charging);
        if (scene.name == "FinalCutscene" || dialogueActive) // prevent input during cutscenes or dialogue
        { }
        else
        {
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
                swordCollider.enabled = true;
                makeAttackSound();
                damage = Mathf.FloorToInt((1 + attackTimeElapsed));
                damaging = true;
                damageDelay = damageDuration;
                charging = false;
                particleFire.Stop();
            }
        }
    }

    private void timeCharge()
    {
        if (attackTimeElapsed < chargeDuration)
        {
            attackTimeElapsed += Time.deltaTime;
        }
    }
}
