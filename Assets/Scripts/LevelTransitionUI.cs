using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransitionUI : MonoBehaviour {
	Text levelText;
	string sceneName;
	float timer = 0.0F;
	float duration = 4.0F;
	bool timerBool;
	
	void Start() {
		levelText = GameObject.Find("LevelTextTransitionUI").GetComponent<Text>();
		Scene currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
		timerBool = true;
	}
	
	void Update() {
		timer += Time.deltaTime; // only at the beginning of each level
		if (timer > duration) {
			timerBool = false;
			gameObject.SetActive(false);
		}
			
		if (sceneName == "Level 1")
			levelText.text = sceneName + ": The Mountain";
		if (sceneName == "Level 2")
			levelText.text = sceneName + ": The Cave";
		if (sceneName == "Level 3")
			levelText.text = sceneName + ": The Maze";
    }
	
}
