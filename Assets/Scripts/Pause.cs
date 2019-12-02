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
    GameObject regularDescription;
    GameObject iceDescription;
    GameObject brickDescription;
    GameObject lightDescription;
    GameObject flameDescription;
    List<int> possessions;
    public bool paused;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // prevent from getting destroyed between scenes
    }

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("UI Canvas");
        pauseMenu = GameObject.Find("MainPauseMenu");
        controlScheme = GameObject.Find("ControlScheme");
        buttons = GameObject.Find("PauseMenuButtons");
        regularDescription = GameObject.Find("RegularSwordDescription");
        iceDescription = GameObject.Find("IceSwordDescription");
        brickDescription = GameObject.Find("BrickSwordDescription");
        lightDescription = GameObject.Find("LightSwordDescription");
        flameDescription = GameObject.Find("FlameSwordDescription");
        possessions = GameObject.Find("Player").GetComponent<Player>().swordPossessions;
        pauseMenu.GetComponent<Canvas>().worldCamera = mainCamera;
        controlScheme.GetComponent<Canvas>().worldCamera = mainCamera;
        buttons.GetComponent<Canvas>().worldCamera = mainCamera;
        ui.SetActive(true);
        pauseMenu.SetActive(false);
        controlScheme.SetActive(false);
        buttons.SetActive(false);
        regularDescription.SetActive(false);
        iceDescription.SetActive(false);
        brickDescription.SetActive(false);
        lightDescription.SetActive(false);
        flameDescription.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (pauseMenu.activeInHierarchy)
        {
            paused = true;
            if (possessions.Contains(0))
            {
                regularDescription.SetActive(true);
            }
            if (possessions.Contains(1))
            {
                flameDescription.SetActive(true);
            }
            if (possessions.Contains(2))
            {
                brickDescription.SetActive(true);
            }
            if (possessions.Contains(3))
            {
                iceDescription.SetActive(true);
            }
            if (possessions.Contains(4))
            {
                lightDescription.SetActive(true);
            }

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
