using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject regularSword;
    [SerializeField] GameObject iceSword;
    [SerializeField] GameObject brickSword;

    bool pickingUpSword;
    bool moving;
    bool grounded;
    bool falling;
    Rigidbody2D player;
    Animator playerAnimator;
    Animator swordAnimator;
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

    public void ChangeToBossScene() {
        if (Input.GetButtonDown("ToBoss")) {
            SceneManager.LoadScene("PlayerBossInteraction");
        }
    }

    void Update()
    {
        MovePlayer();
        CheckFalling();
        SwitchSwords();
        ChangeToBossScene();
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
            // Hide the player's held sword
            SpriteRenderer heldSwordSR = new SpriteRenderer();
            SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
            // GetComponentsInChildren includes the parent object, so if the parent object exists
            // Then we've found a proper child component i.e. the held sword
            foreach (SpriteRenderer sr in srs)
                if (sr.gameObject.transform.parent != null)
                    heldSwordSR = sr;
            if (heldSwordSR != null)
                heldSwordSR.enabled = false; // Hide it
            pickingUpSword = true;
            moving = false;
            col.gameObject.transform.localPosition = new Vector2(transform.position.x, transform.position.y + 1);
            col.gameObject.GetComponentInChildren<Animator>().enabled = false;
            StartCoroutine(WaitAndPickup(col.gameObject, heldSwordSR));
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

    private IEnumerator WaitAndPickup(GameObject swordGO, SpriteRenderer heldSwordSR)
    {
        // Will force a wait before the player can continue playing
        // Mainly so there's a pick-up animation that's held for 2 seconds
        // before the sword is added to the inventory and they can continue
        yield return StartCoroutine(HandleSwordPickup(swordGO, heldSwordSR));
    }

    private IEnumerator HandleSwordPickup(GameObject swordGO, SpriteRenderer heldSwordSR)
    {
        yield return new WaitForSeconds(2.0f); // Will hold the pose for 2 seconds.

        // After this timer, add the new sword to the inventory
        SwordInventory si = GameObject.FindObjectOfType<SwordInventory>();
        if (si != null)
        {
            si.AddSlot(SwordId(swordGO));
            Destroy(swordGO);
        }
        // Stop the animation and make the player's original sword reappear in their hand
        if (heldSwordSR != null)
            heldSwordSR.enabled = true;
        pickingUpSword = false;
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
    
    private int SwordId(GameObject sword)
    {
        string name = sword.name;
        if (name.Contains("Flame"))
            return 1;
        if (name.Contains("Brick"))
            return 2;
        if (name.Contains("Ice"))
            return 3;
        if (name.Contains("Light"))
            return 4;
        if (name.Contains("Guitar"))
            return 5;
        return 0;
    }
}
