using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    Camera mainCamera;
    GameObject levelSelect;
    AudioSource[] music;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        levelSelect = GameObject.Find("LevelSelect");
        music = GameObject.Find("Music").GetComponents<AudioSource>();
    }

    public void level1()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(2);
        }
    }

    public void level2Start()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(3);
        }
    }

    public void level2Middle()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(4);
        }
    }

    public void level2End()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(5);
        }
    }

    public void level3()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(6);
        }
    }

    public void finalBossPart1()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(7);
        }
    }

    public void finalBossPart2()
    {
        if (levelSelect.activeInHierarchy)
        {
            music[0].Stop();
            SceneManager.LoadScene(8);
        }
    }
}
