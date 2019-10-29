using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    Rigidbody2D rigidbody;

    public override void Start() {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        movingRight = true;
    }

    public override void Update() {
        if (movingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
    }

    public override void Die() {

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
    }
}
