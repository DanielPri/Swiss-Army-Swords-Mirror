using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBarSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossLifeBar;
    public bool fightStart;

    void Start()
    {
        fightStart = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && fightStart == false)
        {
            Instantiate(bossLifeBar, new Vector2(0, -7), Quaternion.identity, GameObject.Find("UI Canvas").transform);
            fightStart = true;
        }
    }
}
