using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    [SerializeField]
    GameObject PrefabBossIntro = null;

    BossBar hitpointBar;
    Transform playerPosition;

    float introDuration = 0.9F;
    float spawnedTimer = 0.0F;
    bool isSpawned;

    public override void Start() {
        base.Start();
        hitpointBar = GameObject.Find("BossLifeBar").GetComponent<BossBar>();
        transform.localScale = new Vector3(0, 0, 0); // Hide for the intro
        MorphAnimation();
        playerPosition = GameObject.Find("PlayerTest").GetComponent<Transform>();
        isSpawned = true;
    }

    public override void Update() {
        HandleTimers();
    }

    private void HandleTimers() {
        if (isSpawned) {
            spawnedTimer += Time.deltaTime;
            if (spawnedTimer > introDuration) {
                isSpawned = false;
                transform.localScale = new Vector3(7, 7, 7); // Spawn after the intro
            }
        } else {
            // Follow the player
            Vector2 target = playerPosition.position;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void MorphAnimation() {
        GameObject morph = Instantiate(PrefabBossIntro, transform.position, Quaternion.identity) as GameObject;
        Destroy(morph, introDuration);
    }

    public override void Die() {
        base.Die();
        MorphAnimation();
        // Show some UI here maybe after a boss ?
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Will be used later once we have a player attacking the boss
        if (collider.gameObject.name == "SwordSlash") {
            hitpointBar.DecreaseBossHitpoint(7);
        }
    }
}
