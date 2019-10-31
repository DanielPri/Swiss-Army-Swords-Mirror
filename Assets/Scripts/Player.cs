using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;

    bool pickingUpSword;
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
        playerAnimator.SetBool("isMoving", moving);
        playerAnimator.SetBool("isGrounded", grounded);
        playerAnimator.SetBool("isFalling", falling);
        playerAnimator.SetBool("isPickingUpSword", pickingUpSword);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            grounded = true;
        }
        if (col.gameObject.name.Contains("SwordDrop") && !pickingUpSword)
        {
            pickingUpSword = true;
            StartCoroutine(WaitAndPickup(col.gameObject));
            Debug.Log("Back from sword pickup");
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

    private IEnumerator WaitAndPickup(GameObject swordGO)
    {
        yield return StartCoroutine(HandleSwordPickup(swordGO));
    }

    private IEnumerator HandleSwordPickup(GameObject swordGO)
    {
        Sword swordcomponent = GetComponentInChildren<Sword>();
        swordcomponent.enabled = false;
        yield return new WaitForSeconds(2.0f);        
        SwordInventory si = GetComponentInParent<SwordInventory>();
        Debug.Log(si);
        if (si == null)
        {
            Debug.Log("Found?");
        }
        pickingUpSword = false;
        swordcomponent.enabled = true;
    }

    private void MovePlayer()
    {
        moving = false;
        if (!pickingUpSword)
        {
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
    }

    private void CheckFalling()
    {
        falling = player.velocity.y < 0.0f;
    }

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
}
