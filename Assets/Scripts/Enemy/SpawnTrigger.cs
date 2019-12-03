using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameObject a = Instantiate(prefab) as GameObject;
            a.transform.position = collision.gameObject.transform.position;
        }
    }
}
