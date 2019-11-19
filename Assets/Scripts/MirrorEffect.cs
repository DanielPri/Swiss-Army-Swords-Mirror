using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEffect : MonoBehaviour {
	[SerializeField]
	GameObject mobPrefab;
	
	AudioSource mirrorPuzzleSound; // For last target showing puzzle hint

    void Start() {
		AudioSource[] audioSources = GetComponents<AudioSource>();
        mirrorPuzzleSound = audioSources[0];
    }

    void Update() {
    }
	
	void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Laser(Clone)" && gameObject.tag == "Mirror") {
            collider.gameObject.GetComponent<Laser>().SetDirection(transform.right);
			collider.gameObject.GetComponent<Laser>().mirrorHit++;
        }
		
		if (collider.name == "Laser(Clone)" && gameObject.tag == "BrokenMirror") {
            Destroy(collider.gameObject);
			
			// For level 3 (1st broken mirror) --> spawns a mob as hint
			if (gameObject.transform.position == new Vector3(50.65F, -3.89F, 0.0F) && collider.gameObject.GetComponent<Laser>().mirrorHit > 1) { // At least hit 2 mirrors
				mirrorPuzzleSound.Play();
				GameObject mob = Instantiate(mobPrefab, new Vector3(50.65F, -5.22F, 0.0F), Quaternion.identity) as GameObject;
				mob.transform.localScale = new Vector3(4.0F, 4.0F, 4.0F);
			}
			// For level 3 (2nd broken mirror) --> spawns 2 mobs as a bad hint ;)
			if (gameObject.transform.position == new Vector3(66.7F, 1.84F, 0.0F)) { // No mirror hit needed
				mirrorPuzzleSound.Play();
				GameObject mob1 = Instantiate(mobPrefab, new Vector3(73.5F, 7.34F, 0.0F), Quaternion.identity) as GameObject;
				mob1.transform.localScale = new Vector3(4.0F, 4.0F, 4.0F);
				GameObject mob2 = Instantiate(mobPrefab, new Vector3(78.5F, 9.25F, 0.0F), Quaternion.identity) as GameObject;
				mob2.transform.localScale = new Vector3(4.0F, 4.0F, 4.0F);
			}
			// For level 3 (3nd broken mirror) --> hit the right mirror to destroy the broken mirror (must hit Mirror3 --> Mirror 4 --> Mirror5 --> Mirror6 --> Mirror7 --> Mirror8)
			if (gameObject.transform.position == new Vector3(101.5F, 8.96F, 0.0F) && collider.gameObject.GetComponent<Laser>().mirrorHit > 5) { // at least 6 hit needed
				mirrorPuzzleSound.Play();
				Destroy(gameObject);
			}
			
			collider.gameObject.GetComponent<Laser>().mirrorHit = 0; // We reset
        }
    }
	
}
