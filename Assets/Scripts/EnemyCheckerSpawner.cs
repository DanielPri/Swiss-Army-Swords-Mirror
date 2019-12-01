using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject mob;
    [SerializeField]
    GameObject mobPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mob == null)
        {
            mob = Instantiate(mobPrefab, transform.position, Quaternion.identity);
            mob.transform.localScale = new Vector3(5, 5, 5);
        }
    }
}
