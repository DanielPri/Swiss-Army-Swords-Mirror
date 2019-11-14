using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : Projectile {

    public override void Awake() {
        base.Awake();
    }

    public override void SetDirection(Vector2 direction) {
        base.SetDirection(direction);
    }

    void OnTriggerEnter2D(Collider2D col) {
        // Will be used later once we have a player attacking the boss
        HitpointBar playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        if (col.gameObject.name == "Player") {
            playerHPBar.DecreaseHitpoint(5);
            Destroy(gameObject);
        }
    }
}
