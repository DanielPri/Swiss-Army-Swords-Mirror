using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3;
    [SerializeField] float jumpForce = 50;

    Animator animator;

    bool moving;
    bool grounded;
    bool falling;
    Rigidbody2D player;
    
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moving = false;
        grounded = false;
    }

    void Update()
    {
        MovePlayer();
        CheckFalling();
        // Update animator's variables
        animator.SetBool("isMoving", moving);
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isFalling", falling);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            grounded = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            grounded = false;
        }
    }

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

    private void CheckFalling()
    {
        falling = player.velocity.y < 0.0f;
    }
}
