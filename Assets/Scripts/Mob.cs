﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    [SerializeField]
    GameObject DieParticlePrefab = null;

    HitpointBar playerHPBar;
    Sword sword;
    Rigidbody2D rigidbody;
    SpriteRenderer hurtColor;

    public int easyMobHP = 2;
    public bool isFrozen;
    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;

    AudioSource hitSound;

    public override void Start() {
        base.Start();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        hurtColor = GetComponent<SpriteRenderer>();
        movingRight = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
    }

    public override void Update()
    {
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        if (movingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
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
    }

    public void Hurt() {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
        hitSound.Play();
        easyMobHP--;
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
        if (col.tag == "Sword" && easyMobHP != 0 && sword.damaging)
            isHurt = true;
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
            playerHPBar.DecreaseHitpoint(1);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
