using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEffect : MonoBehaviour {
	[SerializeField]
	GameObject mobPrefab = null;
	
	AudioSource mirrorPuzzleSound; // For last target showing puzzle hint
	AudioSource wrongMirrorPuzzleSound;

    void Start() {
		AudioSource[] audioSources = GetComponents<AudioSource>();
        mirrorPuzzleSound = audioSources[0];
		wrongMirrorPuzzleSound = audioSources[1];	 
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
			
			// For level 3 (4th puzzle with broken mirrors) --> hit 1st, 3rd, 2nd & 4th from the left. If not good, spawn a flying mob sometimes randomly & reset
			if (gameObject.transform.position == new Vector3(170.5F, 30.6F, 0.0F)) {
				if (GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter == 0) {
					RightMirror();
				} else {
					WrongMirror();
				}
			}
			if (gameObject.name == "BrokenMirror5" && gameObject.transform.position == new Vector3(176.5F, 32.5F, 0.0F)) {
				if (GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter == 1) {
					RightMirror();
				} else {
					WrongMirror();
				}
			}
			if (gameObject.name == "BrokenMirror4" && gameObject.transform.position == new Vector3(173.5F, 31.5F, 0.0F)) {
				if (GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter == 2) {
					RightMirror();
				} else {
					WrongMirror();
				}
			}
			if (gameObject.name == "BrokenMirror6" && gameObject.transform.position == new Vector3(179.5F, 33.6F, 0.0F)) {
				if (GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter == 3) {
					RightMirror();
					// Drop Flame Sword
					GameObject flameSword = Instantiate(GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().flameSwordDropPrefab, new Vector3(181.5F, 30.5F, 0.0F), Quaternion.identity) as GameObject;
					Destroy(GameObject.Find("BrokenMirrorDoor2")); // Opens the door
				} else {
					WrongMirror();
				}
			}
			
			collider.gameObject.GetComponent<Laser>().mirrorHit = 0; // We reset
        }
    }
	
	void RightMirror() {
		GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter++;
		mirrorPuzzleSound.Play();
	}
	
	void WrongMirror() {
		GameObject.Find("LevelPuzzles").GetComponent<GamePuzzles>().rightMirrorCounter = 0; // Wrong one
		wrongMirrorPuzzleSound.Play();
	}
}
