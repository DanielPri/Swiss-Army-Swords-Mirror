using UnityEngine;
using System.Collections;

public class LightSword : Sword
{
    public GameObject lightLaserPrefab;
    public ParticleSystem particleLight = null;
    public Light light = null;

    public override void Start() {
        base.Start();
        base.swordType = SwordType.LIGHT;
        particleLight = GetComponentsInChildren<ParticleSystem>()[0];
        light = GetComponentsInChildren<Light>()[0];
        particleLight.Stop();
        light.enabled = false;
    }

    public override void Attack() {
        base.Attack();
        // Laser
    }

    public override void Ability() {
        base.Ability();
        // Light torch
        particleLight.Play();
        light.enabled = true;
    }

    public override void OnTriggerEnter2D(Collider2D collision) {

    }
}
