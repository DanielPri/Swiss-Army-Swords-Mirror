using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    Player player;
    Scene scene;
    bool level1;
    bool level2;
    bool level2puzzle;
    bool level2end;
    bool level3;
    bool finalBoss;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        level1 = false;
        level2 = false;
        level2puzzle = false;
        level2end = false;
        level3 = false;
        finalBoss = false;
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1" && !level1)
        {
            transform.position = new Vector2(3.2f, 7.4f);
            level1 = true;
        }
        if (scene.name == "Level 2" && !level2)
        {
            transform.position = new Vector2(-2.9f, 9.7f);
            level2 = true;
        }
        if (scene.name == "Level 2 Puzzle" && !level2puzzle)
        {
            transform.position = new Vector2(3.99f, -2.38f);
            transform.localScale = new Vector2(-1, 1);
            player.facingDirection = Vector2.left;
            level2puzzle = true;
        }
        if (scene.name == "Level 2 Part 2" && !level2end)
        {
            transform.position = new Vector2(-5.88f, 4.49f);
            level2end = true;
        }
        if (scene.name == "Level 3" && !level3)
        {
            transform.position = new Vector2(-9.2f, -3.42f);
            level3 = true;
        }
        if (scene.name == "DragonBoss" && !finalBoss)
        {
            transform.position = new Vector2(-9.03f, -2.93f);
            finalBoss = true;
        }
    }
}
