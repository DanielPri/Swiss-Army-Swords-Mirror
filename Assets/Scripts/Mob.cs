using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    [SerializeField]
    GameObject DieParticlePrefab = null;

    Sword sword;
    Rigidbody2D rb;
    SpriteRenderer hurtColor;

    public int easyMobHP = 2;
    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;
    HitpointBar playerHPBar;
    Player player;
    AudioSource hitSound;

    new public void Start() {
        base.Start();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        rb = GetComponent<Rigidbody2D>();
        hurtColor = GetComponent<SpriteRenderer>();
        movingRight = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
        
        // Freeze properties
        freezeable = true;
        freezeCubeOffset = new Vector3(0, 0.5f);
        freezeCubeScale = new Vector3(0.1f, 0.2f, 0.1f);
        freezeCubeScale = new Vector3(0.1f, 0.2f, 0.1f);
    }

    new public void Update()
    {
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();

        if (movingRight && !isFrozen)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else if (!isFrozen)
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        HandleTimers();
        if (easyMobHP == 0 && !isFrozen)
            Die();
    }

    private void HandleTimers() {
        if (isHurt) {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration) {
                isHurt = false;
                hurtTimer = 0.0f;
            }
            Hurt();
        }
        if (isFrozen) {
            Freeze();
        }
    }

    public void Hurt() {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
        hitSound.Play();
        easyMobHP--;
    }

    public void Freeze()
    {
        Color firstColor = new Color(165F, 242F, 243F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }

    public override void Die() {
        GameObject particles = Instantiate(DieParticlePrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(particles, 1.0F);
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }

    new void OnTriggerEnter2D(Collider2D col) {
        base.OnTriggerEnter2D(col);
        if (col.tag == "EnemyZone") {
            GetComponent<SpriteRenderer>().flipX = movingRight;
            movingRight = !movingRight;
        }
        if (col.tag == "Sword" && easyMobHP != 0 && sword.damaging) { isHurt = true; }
            
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sword" && easyMobHP != 0 && sword.damaging)
            isHurt = true;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isFrozen)
            {
                playerHPBar.DecreaseHitpoint(1);
            player.IsHurt = true;
                playerHurtSound();
            }
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
