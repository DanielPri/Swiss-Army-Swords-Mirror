using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    [SerializeField]
    GameObject DieParticlePrefab = null;

    HitpointBar playerHPBar;
    Sword sword;
    Rigidbody2D rigidbody;
    SpriteRenderer hurtColor;

    int easyMobHP = 2;
    bool move;
    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;

    AudioSource hitSound;

    public override void Start() {
        base.Start();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        rigidbody = GetComponent<Rigidbody2D>();
        hurtColor = GetComponent<SpriteRenderer>();
        move = true;
        movingRight = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
    }

    public override void Update() {
        if(move)
        {
            if (movingRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
        HandleTimers();
        if (easyMobHP == 0)
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
    }

    public void Hurt() {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
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

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "EnemyZone") {
            GetComponent<SpriteRenderer>().flipX = movingRight;
            movingRight = !movingRight;
        }
        if (col.tag == "Sword" && easyMobHP != 0 && sword.attacking) {
            isHurt = true;
            hitSound.Play();
            easyMobHP--;
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // playerHPBar.DecreaseHitpoint(1);
            move = false;
        }

    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            move = true;
        }
    }
}
