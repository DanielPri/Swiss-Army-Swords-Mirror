using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTile : MonoBehaviour {
	[SerializeField]
	GameObject PlayerTouchedPrefab = null; // When the player touch the lava
	
	HitpointBar playerHPBar;
	GameObject player = null;
	Rigidbody2D  playerRigidbody = null;
    Player playerScript;
	GameObject fire = null;
	SpriteRenderer fireFade = null;
	bool startForceTimer = false;
	
	float pushbackForce = 7.0F;
	float touchedTime = 1.5F; // 3 seconds
	float forceTimer = 0.0F;
	
    public virtual void Start() {
		player = GameObject.Find("Player");
		playerRigidbody = player.GetComponent<Rigidbody2D>();
		playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        playerScript = player.GetComponent<Player>();
    }

    public virtual void Update() { 
		if (startForceTimer) {
			forceTimer += Time.deltaTime;
            if (forceTimer > 1.0F) {
				playerRigidbody.velocity = Vector3.zero;
                forceTimer = 0.0F;
				startForceTimer = false;
				// Give an effect to the fire
				Color firstColor = new Color(0.03F, 0.2F, 0.7F, 1F);
				Color secondColor = new Color(1F, 1F, 1F, 0.1F);
				fireFade = fire.GetComponent<SpriteRenderer>();
				fireFade.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 2.0F, 1.0F));
            }
		}
		// Some fire coming out of lava later here
    }
	
	private void GenerateFire() {
		Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y - 1.0F, player.transform.position.z);
		fire = Instantiate(PlayerTouchedPrefab, position, Quaternion.identity) as GameObject;
		fire.transform.parent = player.transform;
		Destroy(fire, touchedTime);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "Player") {
			startForceTimer = true;
			playerRigidbody.AddForce(new Vector2(0.0F, pushbackForce), ForceMode2D.Impulse);
			GenerateFire();
			playerHPBar.DecreaseHitpoint(10);
            playerScript.IsHurt = true;
            playerScript.audioSource.clip = playerScript.hurtSounds[UnityEngine.Random.Range(0, playerScript.hurtSounds.Length)];
            playerScript.audioSource.Play();
        }
    }
	
}
