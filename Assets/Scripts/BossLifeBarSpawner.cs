using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBarSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossLifeBar;
    public bool fightStart;
    GameObject theBar;

    void Start()
    {
        fightStart = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && fightStart == false)
        {
            theBar = Instantiate(bossLifeBar, new Vector2(0, -200), Quaternion.identity);
            theBar.transform.SetParent(GameObject.Find("UI Canvas").transform, false);
            fightStart = true;
        }
    }
}
