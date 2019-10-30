using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    [SerializeField]
    GameObject DieParticlePrefab = null;

    Rigidbody2D rigidbody;
    SpriteRenderer hurtColor;

    int easyMobHP = 2;
    bool isHurt;
    float hurtTimer = 0.0F;
    float hurtDuration = 2.0F;

    public override void Start() {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        hurtColor = GetComponent<SpriteRenderer>();
        movingRight = true;
    }

    public override void Update() {
        if (movingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
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
        if (col.tag == "Player") {
            // Will decrease the HP bar of player once it is on the same scene
            // Hurt anim + sound also
        }
        if (col.gameObject.name == "Regular Sword" && easyMobHP != 0) {
            // Not complete: we have to check if the sword is animated as well here otherwise the mob gets
            // attacked even if the sword is not moving on the player (will do it later once Sword.cs is not
            // being edited by someone else
            isHurt = true;
            easyMobHP--;
        }
    }
}
