using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField]
    float speed;
    Rigidbody2D rigidbody;
    Transform sprite;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.right * speed;
        sprite = GetComponent<Transform>();
    }

    public void SetDirection(Vector2 direction) {
        rigidbody.velocity = direction * speed;
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        sprite.rotation = rotation3D;
    }
}
