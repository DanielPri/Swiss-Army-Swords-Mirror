using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : Projectile {

    Player playerObject;

    public override void Awake() {
        base.Awake();
        playerObject = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void SetDirection(Vector2 direction) {
        base.SetDirection(direction);
    }

    void OnTriggerEnter2D(Collider2D col) {
        // Will be used later once we have a player attacking the boss
        HitpointBar playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        if (col.gameObject.name == "Player") {
            playerHPBar.DecreaseHitpoint(5);
            playerHurtSound();
            Destroy(gameObject);
        }
    }
    void playerHurtSound()
    {
        playerObject.audioSource.clip = playerObject.hurtSounds[UnityEngine.Random.Range(0, playerObject.hurtSounds.Length)];
        playerObject.audioSource.Play();
    }
}
