using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;
    CanvasRenderer panel;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("MainPauseMenu").GetComponent<Canvas>();
        panel = GameObject.Find("PauseMenuPanel").GetComponent<CanvasRenderer>();
        ui.worldCamera = mainCamera;
    }

    void Update()
    {
        panel.SetAlpha(0.7f);
    }
}
