using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTile : MonoBehaviour {
	Rigidbody2D playerRigidbody;
	bool touchingLava;
	
	float pushbackForce = 20.0F;
	
    public virtual void Start() {
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		touchingLava = false;
    }

    public virtual void Update() { 
		
    }
	
	void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            touchingLava = true;
			playerRigidbody.AddForce(new Vector2(0.0F, pushbackForce), ForceMode2D.Impulse);
        }
    }
	
	void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            touchingLava = false;
        }
    }
	
}
