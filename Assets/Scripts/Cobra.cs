using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Enemy {
	Rigidbody2D rigidbody;
	Animator cobraAnimator;
	SpriteRenderer hurtColor;
	bool idle;
	bool moving;
    bool attacking;
    bool dieing;
	public int mediumHP = 5;
	bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;
	float stateTimer = 2.0F;
	
	public int state = 1; // 1: idle, 2: moving left, 3: moving right, 4: attacking
	
    public override  void Start() {
		base.Start();
		rigidbody = GetComponent<Rigidbody2D>();
        cobraAnimator = GetComponent<Animator>();
		idle = true;
		moving = false;
		attacking = false;
		dieing = false;
    }

    public override void Update() {
		base.Update();
        HandleAnimations();
		GenerateRandomState();
		HandleMovement();
		HandleTimers();
		if (mediumHP == 0)
			Die();
    }
	
	void HandleAnimations() {
		cobraAnimator.SetBool("isIdle", idle);
		cobraAnimator.SetBool("isMoving", moving);
        cobraAnimator.SetBool("isAttacking", attacking);
        cobraAnimator.SetBool("isDieing", dieing);
	}
	
	void GenerateRandomState() {
		if (Time.time > stateTimer) {
            stateTimer = Time.time + Random.Range(3, 6);
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
		} else if (state == 1 && !dieing) {
			rigidbody.velocity = Vector2.zero;
			moving = false;
			idle = true;
		} else if (state == 4 && !dieing) {
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
    }
	
	void Die() {
		dieing = true;
		Destroy(gameObject, 1.0F);
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
}
