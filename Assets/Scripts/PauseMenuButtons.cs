using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("PauseMenuButtons").GetComponent<Canvas>();
        ui.worldCamera = mainCamera;
    }
}
