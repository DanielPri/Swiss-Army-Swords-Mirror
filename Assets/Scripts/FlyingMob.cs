﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingMob : Enemy { // 3 HP, Gives 3 Damages
	[SerializeField]
	Player player = null;
	[SerializeField]
	public float moveSpeed;
	[SerializeField]
	public float playerRange;
	
	HitpointBar playerHPBar;
    Sword sword;
	Rigidbody2D rigidbody;
    SpriteRenderer hurtColor;
	
	public LayerMask playerLayer;
	public bool playerInRange;
	string sceneName;
	
	public int easyMobHP = 3;
    public bool isFrozen;
    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;
	
	AudioSource hitSound;
	
    public override void Start() {
		base.Start();
		Scene currentScene = SceneManager.GetActiveScene(); // To know which level
		sceneName = currentScene.name;
        player = GameObject.Find("Player").GetComponent<Player>();
		playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        hurtColor = GetComponent<SpriteRenderer>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
    }

    public override void Update() {
		// Must be attracted to light of light sword
		base.Update();
		playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
		sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
		if (playerInRange && FindObjectOfType<LightSword>().laserOn == true && sceneName == "Level 2") { // For level 2, they should be attracted to light of light sword
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			FaceDirection(player.transform.position);
			return;
		}
		else if (playerInRange && sceneName == "Level 3") {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			FaceDirection(player.transform.position);
			return;
		}
		HandleTimers();
		if (easyMobHP < 1 && !isFrozen)
            Die();
    }
	
	public override void Die() {
        Destroy(gameObject);
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
        Color firstColor = new Color(1F, 1F, 1F, 1F);
        Color secondColor = new Color(0.2F, 0.009F, 0.002F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
        hitSound.Play();
        easyMobHP--;
    }
	
	public void Freeze() {
        Color firstColor = new Color(165F, 242F, 243F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }
	
	private void FaceDirection(Vector3 playerPosition) {
        // Opposite to the player
        if (transform.position.x < player.transform.position.x) {
            Vector2 newScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
        }
        else {
            Vector2 newScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
        }
    }
	
	// Disable the gizmo in the game
	void OnDrawGizmosSelected() {
		// Makes a circle around player for playerRange
		Gizmos.DrawSphere(transform.position, playerRange);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Sword" && easyMobHP != 0 && sword.damaging)
            isHurt = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sword" && easyMobHP != 0 && sword.damaging)
            isHurt = true;
    }
	
	public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            playerHPBar.DecreaseHitpoint(3);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    public void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
	
}