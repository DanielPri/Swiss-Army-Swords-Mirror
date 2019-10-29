using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {

    public override void Start() {
        base.Start();
        movingRight = true;
    }

    public override void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Die() {

    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
