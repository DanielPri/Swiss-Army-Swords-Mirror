using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    protected float speed;

    protected bool movingRight;

    public virtual void Start() {
    }

    public virtual void Update() { 
    }

    public virtual void Die() {
        transform.localScale = new Vector3(0, 0, 0);
        OnDestroy();
    }

    public virtual void SetSpeed(float number) {
        speed = number;
    }

    public virtual void OnDestroy()
    {
        // We can delete enemies in a list later here
    }
}
