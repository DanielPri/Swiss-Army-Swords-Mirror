using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Enemy {
	Animator cobraAnimator;
	bool idle;
	bool moving;
    bool attacking;
    bool dieing;
	public int mediumHP = 5;
	SpriteRenderer hurtColor;
	bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;
	
	public int state = 1; // 1: idle, 2: moving left, 3: moving right, 4: attacking
	
    public override  void Start() {
		base.Start();
        cobraAnimator = GetComponent<Animator>();
		idle = true;
		moving = false;
		attacking = false;
		dieing = false;
    }

    public override void Update() {
		base.Update();
        HandleAnimations();
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
	
	void HandleAttacking() { // Will have to make it so once near the snake, attack
		if (state == 4)
			attacking = true;
	}
	
	void HandleMovement() {
		if (state == 3 && !dieing) {
			moving = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else if (state == 2 && !dieing) {
			moving = true;
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
		} else {
			moving = false;
			idle = true;
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
