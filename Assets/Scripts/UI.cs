using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // prevent from getting destroyed between scenes
    }

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        ui.worldCamera = mainCamera;
    }
}
