using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile {
    [SerializeField]
    GameObject ParticlesPrefab = null;

    SpriteRenderer effectColor;

    bool destroyed;
	public int mirrorHit = 0; // Counts how many mirrors are hit for checks

    public override void Awake() {
        base.Awake();
        effectColor = GetComponent<SpriteRenderer>();
    }

    public override void Update() {
		base.Update();
        LaserEffect();

        if (destroyed)
            Destroyed();
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
        if (col.gameObject.tag == "Enemy") {
			if (col.gameObject.name == "Mob(Clone)" || col.gameObject.name == "Mob") {
				Mob mob = col.gameObject.GetComponent<Mob>();
				mob.Hurt();
				destroyed = true;
			}
			else if (col.gameObject.name.Contains("FlyingMob")) {
				FlyingMob mob = col.gameObject.GetComponent<FlyingMob>();
				mob.Hurt();
				destroyed = true;
			}
        }
    }

    public void Destroyed() {
        GameObject particles = Instantiate(ParticlesPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(particles, 2.0F);
    }
}
