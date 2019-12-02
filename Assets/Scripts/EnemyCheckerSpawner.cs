using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject mob;
    [SerializeField]
    GameObject mobPrefab;
    [SerializeField]
    float waitTimer;
    float waitTime;
    bool spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mob == null && spawn)
        {
            mob = Instantiate(mobPrefab, transform.position, Quaternion.identity);
            mob.transform.localScale = new Vector3(5, 5, 5);
            spawn = false;
        }
        else if (mob == null)
            HandleTimer();
    }

    private void HandleTimer()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= waitTimer)
        {
            spawn = true;
            waitTime = 0.0f;
        }

    }
}
