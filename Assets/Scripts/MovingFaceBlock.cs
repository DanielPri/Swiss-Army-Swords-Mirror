using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFaceBlock : MonoBehaviour
{
    private GameObject dragon;
    private Transform[] dragonChildren;

    // Start is called before the first frame update
    void Start()
    {
        dragon = GameObject.Find("Dragon");
        dragonChildren = dragon.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            foreach (Transform child in dragonChildren)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.collider.transform.SetParent(transform);
            float step = 1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.43f, transform.position.y, 0), step);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
