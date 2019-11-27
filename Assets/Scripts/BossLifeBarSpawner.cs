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
            GameObject camera = GameObject.Find("Main Camera");
            GameObject thebar = Instantiate(bossLifeBar, camera.transform);
            thebar.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 4, 0);
            fightStart = true;
        }
    }
}
