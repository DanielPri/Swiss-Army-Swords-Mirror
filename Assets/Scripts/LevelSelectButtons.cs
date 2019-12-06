using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtons : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("LevelSelectButtons").GetComponent<Canvas>();
        ui.worldCamera = mainCamera;
    }
}
