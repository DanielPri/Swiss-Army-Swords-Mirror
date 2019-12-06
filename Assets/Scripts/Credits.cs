using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;
    CanvasRenderer panel;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("Credits").GetComponent<Canvas>();
        panel = GameObject.Find("CreditsPanel").GetComponent<CanvasRenderer>();
        ui.worldCamera = mainCamera;
    }

    void Update()
    {
        panel.SetAlpha(0.7f);
    }
}
