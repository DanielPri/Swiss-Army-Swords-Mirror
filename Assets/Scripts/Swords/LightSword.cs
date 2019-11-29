using UnityEngine;
using System.Collections;

public class LightSword : Sword
{
    public GameObject lightLaserPrefab;
    public ParticleSystem particleLight = null;
    public Light light = null;

    float lightIntensity, minimumIntensity = 2.0F, maximumIntensity = 6.0F;
    public bool laserOn;
    float projectileDuration = 10.0F;

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
        // Light torch toggle on/off
        if (!laserOn) {
            particleLight.Play();
            // Make enemies do something towards the light later
            light.enabled = true;
            laserOn = true;
            // Random light intensity (need a diffuse background to see it)
            lightIntensity = Random.Range(minimumIntensity, maximumIntensity);
            light.intensity = lightIntensity;
        }
        else {
            particleLight.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            light.enabled = false;
            laserOn = false;
        }
    }

    public void ShootLaser() {
        GameObject laser = Instantiate(lightLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        Laser projectileLaser = laser.GetComponent<Laser>();
        Vector2 direction = player.GetFacingDirection();
        if (direction.x == 0)
            direction.x = 1;
        projectileLaser.SetDirection(direction);
        //Add sound for laser later
        Destroy(laser, projectileDuration);
    }

    public override void OnTriggerEnter2D(Collider2D collision) {

    }
}
