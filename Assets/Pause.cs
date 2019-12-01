using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    Camera mainCamera;
    GameObject ui;
    GameObject pauseMenu;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // prevent from getting destroyed between scenes
    }

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("UI Canvas");
        ui.SetActive(true);
        pauseMenu = GameObject.Find("ControlScheme");
        pauseMenu.GetComponent<Canvas>().worldCamera = mainCamera;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        ui.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        ui.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
