using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSword : Sword
{
    public ParticleSystem particleFire = null;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.FIRE;
        player = GameObject.Find("Player").GetComponent<Player>();
        particleFire = GetComponentsInChildren<ParticleSystem>()[0];
    }


    public override void Ability()
    {
        base.Ability();
        swordAnimator.SetBool("charging", true);
    }
}
