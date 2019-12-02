using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBarSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossLifeBar;
    GameObject theBar;

    void Start()
    {
        theBar = Instantiate(bossLifeBar, new Vector2(0, -200), Quaternion.identity);
        theBar.transform.SetParent(GameObject.Find("UI Canvas").transform, false);
    }
}
