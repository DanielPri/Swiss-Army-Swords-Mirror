using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    Player player;
    BoxCollider2D col;
    float right = 0.25f;
    float left = 0.61f;
    float colleft = -0.15f;
    float colright = 0.18f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;
        Vector2 direction = player.facingDirection;
        if (direction.x == 0)
            direction.x = 1;
        if (direction == Vector2.right)
        {
            newPos = new Vector3(right, transform.localPosition.y, 1);
            col.offset = new Vector2(colright, col.offset.y);
        }
        else
        {
            newPos = new Vector3(left, transform.localPosition.y, 1);
            col.offset = new Vector2(colleft, col.offset.y);
        }

        transform.localPosition = newPos;
    }
}
