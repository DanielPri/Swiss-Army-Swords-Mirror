using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	public PlayableDirector director;
	string sceneName;

    void Start() {
        Scene currentScene = SceneManager.GetActiveScene(); // To know which level
		sceneName = currentScene.name;
    }

    void Update() {
		if (sceneName == "Cutscene") {
			if (director.time > 28.4) { // If cutscene done, move to level 1 (28 seconds)
				SceneManager.LoadScene("Level 1");
			}
			if (Input.GetButton("SkipCutscene")) { // Skip cutscene by pressing "s"
				director.time = 28.0;
			}
		} else if (sceneName == "FinalCutscene") {
			if (director.time > 48.4) { // If cutscene done, move to menu
				SceneManager.LoadScene("MainMenu");
			}
		}
    }
}
