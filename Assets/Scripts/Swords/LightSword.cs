using UnityEngine;
using System.Collections;

public class LightSword : Sword
{
    public GameObject lightLaserPrefab;
    public ParticleSystem particleLight = null;
    public Light light = null;
    Player player = null;
    float projectileDuration = 4.0F;

    public override void Start() {
        base.Start();
        base.swordType = SwordType.LIGHT;
        player = GameObject.Find("Player").GetComponent<Player>();
        particleLight = GetComponentsInChildren<ParticleSystem>()[0];
        light = GetComponentsInChildren<Light>()[0];
        particleLight.Stop();
        light.enabled = false;
    }

    public override void Attack() {
        base.Attack();
        // Laser
        ShootLaser();
    }

    public override void Ability() {
        base.Ability();
        // Light torch
        particleLight.Play();
        light.enabled = true;
    }

    public void ShootLaser() {
        GameObject laser = Instantiate(lightLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        Laser projectileLaser = laser.GetComponent<Laser>();
        projectileLaser.SetDirection(player.GetFacingDirection());
        //Add sound for laser later
        Destroy(laser, projectileDuration);
        
    }

    public override void OnTriggerEnter2D(Collider2D collision) {

    }
}
