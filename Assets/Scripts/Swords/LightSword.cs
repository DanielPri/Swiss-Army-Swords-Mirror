using UnityEngine;
using System.Collections;

public class LightSword : Sword
{
    public GameObject lightLaserPrefab;
    public ParticleSystem particleLight = null;

    public override void Start() {
        base.Start();
        base.swordType = SwordType.LIGHT;
        particleLight = GetComponentsInChildren<ParticleSystem>()[0];
        particleLight.Stop();
    }

    public override void Attack() {
        base.Attack();
        // Laser
    }

    public override void Ability() {
        base.Ability();
        // Light torch
        particleLight.Play();
    }

    public override void OnTriggerEnter2D(Collider2D collision) {

    }
}
