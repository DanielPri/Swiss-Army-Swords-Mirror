using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile {

    SpriteRenderer effectColor;

    public override void Awake() {
        base.Awake();
        effectColor = GetComponent<SpriteRenderer>();
    }

    void Update() {
        LaserEffect();
    }

    public override void SetDirection(Vector2 direction) {
        base.SetDirection(direction);
    }

    public void LaserEffect() {
        Color firstColor = new Color(1.0F, 1.0F, 0.0F, 1.0F);
        Color secondColor = new Color(1.0F, 1.0F, 0.8F, 0.1F);
        effectColor.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 10.0F, 1.0F));
    }

    void OnTriggerEnter2D(Collider2D col) {

    }

}
