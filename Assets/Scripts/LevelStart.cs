using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    Transform player;
    Scene scene;
    bool level1;
    bool level2;
    bool level2puzzle;
    bool level2end;
    bool level3;
    bool finalBoss;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        level1 = false;
        level2 = false;
        level2end = false;
        level3 = false;
        finalBoss = false;
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1" && !level2)
        {
            player.position = new Vector2(3.2f, 7.4f);
            level1 = true;
        }
        if (scene.name == "Level 2" && !level2)
        {
            player.position = new Vector2(-3.65f, 0.4f);
            level2 = true;
        }
        if (scene.name == "Level 2 Part 2" && !level2end)
        {
            player.position = new Vector2(-5.88f, 4.49f);
            level2end = true;
        }
        if (scene.name == "Level 3" && !level3)
        {
            player.position = new Vector2(-9.2f, -3.42f);
            level3 = true;
        }
        if (scene.name == "DragonBoss" && !finalBoss)
        {
            player.position = new Vector2(-9.03f, -2.93f);
            finalBoss = true;
        }
    }
}
