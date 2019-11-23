using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMob : Enemy {
	[SerializeField]
	Player player;
	[SerializeField]
	public float moveSpeed;
	[SerializeField]
	public float playerRange;
	
	public LayerMask playerLayer;
	public bool playerInRange;
	
    public override void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void Update() {
		// Must be attracted to light of light sword
		playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
		if (playerInRange && FindObjectOfType<LightSword>().laserOn) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			FaceDirection(player.transform.position);
			return;
		}
    }
	
	private void FaceDirection(Vector3 playerPosition) {
        // Opposite to the player
        if (transform.position.x < player.transform.position.x) {
            Vector2 newScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
        }
        else {
            Vector2 newScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
        }
    }
	
	// Disable the gizmo in the game
	void OnDrawGizmosSelected() {
		// Makes a circle around player for playerRange
		Gizmos.DrawSphere(transform.position, playerRange);
	}
	
}
