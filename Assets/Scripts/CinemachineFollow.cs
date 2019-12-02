using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFollow : MonoBehaviour
{
    Transform player;
    CinemachineVirtualCamera vcam;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        vcam.Follow = player;
    }
}
