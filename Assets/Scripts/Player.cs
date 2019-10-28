using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float jumpForce = 50;

    bool moving;
    public bool grounded;
    Rigidbody2D player;
    
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        moving = false;
        grounded = true;
    }

    void Update()
    {
        //PlayerGrounded();
        MovePlayer();
    }

    /*private void PlayerGrounded()
    {
        float radius = GetComponent<CapsuleCollider2D>().size.x * 0.9f;
        Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
        grounded = Physics.CheckSphere(pos, radius, 0);
    }*/

    private void MovePlayer()
    {
        moving = false;
        if (Input.GetButton("Left"))
        {
            transform.Translate(-Vector2.right * playerSpeed * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
            moving = true;
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = false;
            moving = true;
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            player.AddForce(Vector2.up * jumpForce);
        }
    }
}
