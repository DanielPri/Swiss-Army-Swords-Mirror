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
		
		if (collider.name == "Laser(Clone)" && gameObject.tag == "BrokenMirror" && collider.gameObject.GetComponent<Laser>().mirrorHit > 1) { // At least hit 2 mirrors
			mirrorPuzzleSound.Play();
            Destroy(collider.gameObject);
			
			// For level 3 --> spawns a mob as hint
			if (gameObject.transform.position == new Vector3(50.65F, -3.89F, 0.0F)) {
				GameObject mob = Instantiate(mobPrefab, new Vector3(50.65F, -5.22F, 0.0F), Quaternion.identity) as GameObject;
				mob.transform.localScale = new Vector3(4.0F, 4.0F, 4.0F);
			}
			collider.gameObject.GetComponent<Laser>().mirrorHit = 0; // We reset
        }
    }
	
}
