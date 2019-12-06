using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool onlyOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !onlyOnce)
        {
            GameObject a = Instantiate(prefab) as GameObject;
            a.transform.position = collision.gameObject.transform.position;
            onlyOnce = true;
        }
    }
}
