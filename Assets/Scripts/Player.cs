using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject regularSword;
    [SerializeField] GameObject iceSword;
    [SerializeField] GameObject brickSword;

    bool moving;
    bool grounded;
    bool falling;
    Rigidbody2D player;
    Animator playerAnimator;
    Vector2 facingDirection;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        facingDirection = transform.right;
        moving = false;
        grounded = false;
        falling = false;
    }

    void Update()
    {
        MovePlayer();
        CheckFalling();
        SwitchSwords();
        playerAnimator.SetBool("isMoving", moving);
        playerAnimator.SetBool("isGrounded", grounded);
        playerAnimator.SetBool("isFalling", falling);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            grounded = true;
        }
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Lever")
        {
            if (Input.GetButtonDown("Fire2"))
            {
                col.GetComponent<LeverDoorController>().toggle();
            }
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
            facingDirection = -transform.right;
            moving = true;
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            transform.localScale = new Vector2(1, 1);
            facingDirection = transform.right;
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

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }

    private void SwitchSwords()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (transform.GetChild(0).name == "Regular Sword")
            {
                Destroy(GameObject.Find("Regular Sword"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(iceSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(iceSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }                
            }
            if (transform.GetChild(0).name == "Ice Sword")
            {
                Destroy(GameObject.Find("Ice Sword"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(brickSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(brickSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
            }
            if (transform.GetChild(0).name == "Brick Sword")
            {
                Destroy(GameObject.Find("Brick Sword"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(regularSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(regularSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
            }
            if (transform.GetChild(0).name == "Regular Sword(Clone)")
            {
                Destroy(GameObject.Find("Regular Sword(Clone)"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(iceSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(iceSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
            }
            if (transform.GetChild(0).name == "Ice Sword(Clone)")
            {
                Destroy(GameObject.Find("Ice Sword(Clone)"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(brickSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(brickSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
            }
            if (transform.GetChild(0).name == "Brick Sword(Clone)")
            {
                Destroy(GameObject.Find("Brick Sword(Clone)"));
                if (facingDirection == (Vector2)(transform.right))
                {
                    Instantiate(regularSword, new Vector2(transform.position.x + 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
                if (facingDirection == (Vector2)(-transform.right))
                {
                    Instantiate(regularSword, new Vector2(transform.position.x - 0.1881f, transform.position.y - 0.1873f), Quaternion.identity, GameObject.Find("Player").transform);
                }
            }
        }
    }
}
