using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Enemy {
	Rigidbody2D rigidbody;
	Animator cobraAnimator;
	SpriteRenderer hurtColor;
	Sword sword;
	HitpointBar playerHPBar;
	
	public int mediumHP = 5; // Bigger life
	public bool isFrozen;
	bool moving;
    bool attacking;
    bool dieing;
	bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;
	float attackingTimer = 0.0F;
	float attackingDuration = 0.5F;
	float stateTimer = 2.0F;
	
	public int state = 1; // 1: idle, 2: moving left, 3: moving right, 4: attacking
	
    public override  void Start() {
		base.Start();
		playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
		rigidbody = GetComponent<Rigidbody2D>();
        cobraAnimator = GetComponent<Animator>();
		hurtColor = GetComponent<SpriteRenderer>();
		moving = false;
		attacking = false;
		dieing = false;
    }

    public override void Update() {
		base.Update();
		sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        HandleAnimations();
		GenerateRandomState();
		HandleMovement();
		HandleTimers();
		if (mediumHP == 0 && !isFrozen)
			Die();
    }
	
	void HandleAnimations() {
		cobraAnimator.SetBool("isMoving", moving);
        cobraAnimator.SetBool("isAttacking", attacking);
        cobraAnimator.SetBool("isDieing", dieing);
	}
	
	void GenerateRandomState() {
		if (Time.time > stateTimer) {
            stateTimer = Time.time + Random.Range(1, 2);
			state = Random.Range(1, 5);
		}
	}
	
	void HandleMovement() {
		if (state == 3 && !dieing) {
			moving = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
			GetComponent<SpriteRenderer>().flipX = false;
        } else if (state == 2 && !dieing) {
			moving = true;
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
			GetComponent<SpriteRenderer>().flipX = true;
		} else if ((state == 1 && !dieing) || (!moving && !dieing)) {
			rigidbody.velocity = Vector2.zero;
			moving = false;
		} else if (state == 4 && !dieing) {
			moving = false;
			attacking = true;
		}
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
		if (attacking) {
            attackingTimer += Time.deltaTime;
            if (attackingTimer >= attackingDuration) {
                attacking = false;
				state = 1; // becomes idle
                attackingTimer = 0.0f;
            }
        }
		if (isFrozen) {
            Freeze();
        }
    }
	
	public void Freeze() {
        Color firstColor = new Color(165F, 242F, 243F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }
	
	void Die() {
		dieing = true;
		Destroy(gameObject, 1.65F);
	}
	
	public void Hurt() {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        hurtColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
        mediumHP--;
    }
	
	public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
	
	void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Sword" && mediumHP != 0 && sword.damaging)
            isHurt = true;
    }

    private void OnTriggerStay2D(Collider2D col)  {
        if (col.tag == "Sword" && mediumHP != 0 && sword.damaging)
            isHurt = true;
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            playerHPBar.DecreaseHitpoint(7);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    public void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
