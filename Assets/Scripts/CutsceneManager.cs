using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	public PlayableDirector director;

    void Start() {
        
    }

    void Update() {
        if (director.state != PlayState.Playing) { // If cutscene done, move to level 1
			SceneManager.LoadScene("Level 1");
		}
    }
}
