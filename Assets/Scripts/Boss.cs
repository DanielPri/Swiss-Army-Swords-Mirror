using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    [SerializeField]
    GameObject PrefabBossIntro = null;
    [SerializeField]
    GameObject PrefabProjectile = null;

    BossBar hitpointBar;
    Transform playerPosition;
    Vector2 facingDirection;

    float projectileDuration = 3.0F;
    float projectileSpeed = 2.0F;

    float introDuration = 0.9F;
    float spawnedTimer = 0.0F;
    float projectileFrequency = 0.0F;
    float nextProjectileSpawn = 0.0F;
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
        projectileFrequency = Random.Range(1, 7);
        HandleTimers();
        HandleProjectiles();
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
            FaceDirection(target);
        }
    }

    private void HandleProjectiles() {
        if (Time.time > nextProjectileSpawn) {
            nextProjectileSpawn = Time.time + projectileFrequency;
            Shoot();
        }
    }

    private void Shoot() {
        if (!isSpawned) {
            GameObject projectileObject = Instantiate(PrefabProjectile, transform.position, Quaternion.identity) as GameObject;
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.SetDirection(GetFacingDirection());
            Destroy(projectile, introDuration);
        }
    }

    private void MorphAnimation() {
        GameObject morph = Instantiate(PrefabBossIntro, transform.position, Quaternion.identity) as GameObject;
        Destroy(morph, introDuration);
    }

    private void FaceDirection(Vector2 direction) {
        // Opposite to the player
        facingDirection = direction;
        if (direction == Vector2.right) {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }

    public Vector2 GetFacingDirection() {
        return facingDirection;
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
