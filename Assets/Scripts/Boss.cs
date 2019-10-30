using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    [SerializeField]
    GameObject PrefabBossIntro = null;
    [SerializeField]
    GameObject PrefabProjectile = null;

    BossBar hitpointBar;
    HitpointBar playerHPBar;
    Rigidbody2D rigidbody;
    Transform playerPosition;
    Vector2 facingDirection;

    SpriteRenderer hurtColor;

    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;

    float projectileDuration = 3.0F;
    float projectileSpeed = 2.0F;

    float introDuration = 0.9F;
    float spawnedTimer = 0.0F;
    float projectileFrequency = 0.0F;
    float nextProjectileSpawn = 0.0F;
    bool isSpawned;

    AudioSource projectileSound;
    AudioSource morphSound;

    public override void Start() {
        base.Start();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        hitpointBar = GameObject.Find("BossLifeBar").GetComponent<BossBar>();
        hurtColor = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        transform.localScale = new Vector3(0, 0, 0); // Hide for the intro
        isSpawned = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        projectileSound = audioSources[0];
        morphSound = audioSources[1];
        MorphAnimation();
    }

    public override void Update() {
        projectileFrequency = Random.Range(1, 7);
        HandleTimers();
        HandleProjectiles();
        if (hitpointBar.GetHP() < 1)
            Die();
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
            Vector2 target = playerPosition.position - transform.position;
            transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
            FaceDirection(-playerPosition.right);
        }
        if (isHurt) {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration) {
                isHurt = false;
                hurtTimer = 0.0f;
            }
            Hurt();
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
            projectileSound.Play();
            Destroy(projectileObject, projectileDuration);
        }
    }

    private void MorphAnimation() {
        GameObject morph = Instantiate(PrefabBossIntro, transform.position, Quaternion.identity) as GameObject;
        morphSound.Play();
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

    public void Hurt() {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }

    public override void Die() {
        base.Die();
        MorphAnimation();
        Destroy(gameObject);
        // Show some UI here maybe after a boss ?
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }

    void OnTriggerEnter2D(Collider2D col) {
        // Will be used later once we have a player attacking the boss
        if (col.gameObject.name == "Regular Sword") {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(5);
        }
        if (col.gameObject.name == "Player") {
            rigidbody.velocity = Vector2.zero;
            // Have to make the boss stop moving here
        }
    }
}
