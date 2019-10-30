using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;

    public bool moving;
    public bool grounded;
    Rigidbody2D player;
    Animator playerAnimator;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        moving = false;
        grounded = false;
    }

    void Update()
    {
        MovePlayer();
        playerAnimator.SetBool("moving", moving);
        playerAnimator.SetBool("grounded", grounded);
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
            transform.localScale = new Vector2(-1, 1);
            moving = true;
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector2(1, 1);
            moving = true;
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            player.AddForce(Vector2.up * jumpForce);
        }
    }
}
