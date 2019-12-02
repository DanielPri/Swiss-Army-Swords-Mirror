using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    Camera mainCamera;
    GameObject ui;
    GameObject pauseMenu;
    GameObject controlScheme;
    GameObject buttons;
    public bool paused;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // prevent from getting destroyed between scenes
    }

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("UI Canvas");
        ui.SetActive(true);
        pauseMenu = GameObject.Find("MainPauseMenu");
        pauseMenu.GetComponent<Canvas>().worldCamera = mainCamera;
        pauseMenu.SetActive(false);
        controlScheme = GameObject.Find("ControlScheme");
        controlScheme.GetComponent<Canvas>().worldCamera = mainCamera;
        controlScheme.SetActive(false);
        buttons = GameObject.Find("PauseMenuButtons");
        buttons.GetComponent<Canvas>().worldCamera = mainCamera;
        buttons.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.gameObject.name);
        }
        if (pauseMenu.activeInHierarchy)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !controlScheme.activeInHierarchy)
        {
            PauseControl();
        }
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)) && controlScheme.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            buttons.SetActive(true);
            controlScheme.SetActive(false);
        }
    }

    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {

            Time.timeScale = 0;
            ui.SetActive(false);
            pauseMenu.SetActive(true);
            buttons.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            ui.SetActive(true);
            pauseMenu.SetActive(false);
            buttons.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }

    public void ExitLevel()
    {
        string name = "MainMenu";
        SceneManager.LoadScene(name);
    }

    public void ControlScheme()
    {
        pauseMenu.SetActive(false);
        buttons.SetActive(false);
        controlScheme.SetActive(true);
    }

}
