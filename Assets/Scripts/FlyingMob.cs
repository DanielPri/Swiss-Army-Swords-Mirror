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
	public bool followOnAway;
	
    public override void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void Update() {
		playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
		if (playerInRange && !followOnAway) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			return;
		}
    }
	
	// Disable the gizmo in the game
	void OnDrawGizmosSelected() {
		// Makes a circle around player for playerRange
		Gizmos.DrawSphere(transform.position, playerRange);
	}
	
}
