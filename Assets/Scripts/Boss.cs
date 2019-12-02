using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    [SerializeField]
    GameObject PrefabBossIntro = null;
    [SerializeField]
    GameObject PrefabProjectile = null;
    [SerializeField]
    float invulnerabilityTime = 0.6f;
    [SerializeField] GameObject dropSword;

    BossBar hitpointBar;
	BossLifeBarSpawner BossLifeBarSpawner;
    HitpointBar playerHPBar;
    Rigidbody2D rb;
    Rigidbody2D playerRigidBody;
    Player playerGO;
    Sword sword;
    Transform playerPosition;
    Vector2 facingDirection;
    

    SpriteRenderer hurtColor;

    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 0.6F;

    float projectileDuration = 3.0F;
    float projectileSpeed = 2.0F;

    float introDuration = 0.8F;
    float spawnedTimer = 0.0F;
    float projectileFrequency = 0.0F;
    float nextProjectileSpawn = 0.0F;
    float distanceFromPlayer = 0;
    bool isSpawned;

    float invulnerabilityTimer = 0.0f;
    bool isInvulnerable = false;

    AudioSource projectileSound;
    AudioSource morphSound;

    new void Start() {
        base.Start();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        hurtColor = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerGO = GameObject.Find("Player").GetComponent<Player>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        transform.localScale = new Vector3(0, 0, 0); // Hide for the intro
        isSpawned = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        projectileSound = audioSources[0];
        morphSound = audioSources[1];
        MorphAnimation();
        
    }

    public override void Update()
    {
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        projectileFrequency = UnityEngine.Random.Range(1, 7);
        HandleTimers();
        HandleProjectiles();
        if (hitpointBar == null)
            hitpointBar = GameObject.Find("BossLifeBar(Clone)").GetComponent<BossBar>();
		else {
			if (hitpointBar.GetHP() < 1)
				Die();
		}
        GetDistanceFromPlayer();
    }

    private void GetDistanceFromPlayer()
    {
        distanceFromPlayer = (transform.position - playerGO.transform.position).magnitude;
    }

    private void HandleTimers() {
        if (isSpawned)
        {
            spawnedTimer += Time.deltaTime;
            if (spawnedTimer > introDuration)
            {
                isSpawned = false;
                transform.localScale = new Vector3(7, 7, 7); // Spawn after the intro
            }
        }
        else if(distanceFromPlayer > 3)
        {
            // Follow the player
            Vector2 target = playerPosition.position - transform.position;
            transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
            playerGO = GameObject.Find("Player").GetComponent<Player>();
            FaceDirection(playerGO.transform.position);
        }
        else // move to height of player to accurately shoot projectiles
        {
            if (transform.position.y > playerPosition.position.y)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
                playerGO = GameObject.Find("Player").GetComponent<Player>();
                FaceDirection(playerGO.transform.position);
            }
            else if (transform.position.y < playerPosition.position.y)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
                playerGO = GameObject.Find("Player").GetComponent<Player>();
                FaceDirection(playerGO.transform.position);
            }
        }
        if (isHurt) {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration) {
                isHurt = false;
                hurtTimer = 0.0f;
            }
            Hurt();
        }
        else
        {
            hurtColor.color = new Color(1F, 1F, 1F, 1F);
        }
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if(invulnerabilityTimer > invulnerabilityTime)
            {
                isInvulnerable = false;
                invulnerabilityTimer = 0.0f;
            }
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
            BossSpell projectile = projectileObject.GetComponent<BossSpell>();
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

    private void FaceDirection(Vector3 playerPosition) {
        // Opposite to the player
        if (transform.position.x < playerPosition.x)
        {
            Vector2 newScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            facingDirection = transform.right;
        }
        else
        {
            Vector2 newScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            facingDirection = -transform.right;
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
        hitpointBar.index = -1;
        MorphAnimation();
        Instantiate(dropSword, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // Show some UI here maybe after a boss ?
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sword" && sword.damaging && !isInvulnerable)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(2);
            isInvulnerable = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Debug.Log("FORCE PUSH ");
            // Boss knocks back player upon collision
            Vector2 forceDirection = new Vector2(facingDirection.x, 1.0f) * 2f;
            playerRigidBody.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }
}
