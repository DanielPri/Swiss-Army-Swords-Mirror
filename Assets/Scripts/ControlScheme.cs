using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlScheme : MonoBehaviour
{
    Camera mainCamera;
    Canvas ui;
    CanvasRenderer panel;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ui = GameObject.Find("ControlScheme").GetComponent<Canvas>();
        panel = GameObject.Find("ControlSchemePanel").GetComponent<CanvasRenderer>();
        ui.worldCamera = mainCamera;
    }

    void Update()
    {
        panel.SetAlpha(0.7f);
    }
}
