using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask platformLayerMask;

    bool pickingUpSword;
    bool moving;
    bool grounded;
    bool falling;
    Rigidbody2D player;
    Animator playerAnimator;
    Animator swordAnimator;
    public Vector2 facingDirection;
    CapsuleCollider2D playerCollider;

    SwordInventory inventory;
    GameObject inventoryGO;
    List<Transform> swords = new List<Transform>();
    List<int> swordPossessions = new List<int>();
    int activeSwordIndex;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // prevent from getting destroyed between scenes
    }

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        moving = false;
        grounded = false;
        falling = false;

        inventoryGO = GameObject.Find("InventoryManager");
        inventory = inventoryGO.GetComponent<SwordInventory>();
        swordPossessions.Add(0);
        getInventorySwords();

        activeSwordIndex = inventory.index;
    }

    private void getInventorySwords()
    {
        // Only have regular sword so set that as active
        if (swordPossessions.Count == 1)
        {
            swords.Add(transform.GetChild(0));
            activeSwordIndex = 0;
            return;
        }

        // get all the sword prefabs within the player game object
        for (int i = 0; i < transform.childCount; i++)
        {
            // if sword is already in inventory, do not add it again
            if (swordPossessions.Contains(i) && !swords.Contains(transform.GetChild(i)))
            {
                swords.Add(transform.GetChild(i));
            }
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                // Keep track of which sword in the array is active
                activeSwordIndex = i;
            }
        }
    }

    private bool findSwordInInventory(Transform curSword)
    {
        foreach (GameObject sword in inventory.inventoryList)
        {
            string name = curSword.name.Replace(" ", "");
            Debug.Log(name);
            if (name == sword.name)
                return true;
        }
        return false;
    }

    public void ChangeToBossScene() {
        if (Input.GetButtonDown("ToBoss")) {
            SceneManager.LoadScene("PlayerBossInteraction");
        }
    }

    void Update()
    {
        isGrounded();
        MovePlayer();
        CheckFalling();
        SwitchSwords();
        ChangeToBossScene();
        playerAnimator.SetBool("isMoving", moving);
        playerAnimator.SetBool("isGrounded", grounded);
        playerAnimator.SetBool("isFalling", falling);
        playerAnimator.SetBool("isPickingUpSword", pickingUpSword);

        activeSwordIndex = inventory.index;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("SwordDrop") && !pickingUpSword)
        {
            // Cannot switch swords until inventory is updated
            inventory.switchSwords = false;
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
            // Hold sword above head - sorta buggy when you jump and collect it
            col.gameObject.transform.localPosition = new Vector2(transform.position.x, transform.position.y + 1);
            col.gameObject.GetComponentInChildren<Animator>().enabled = false;

            swordPossessions.Add(SwordId(col.gameObject));

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
        getInventorySwords(); // Get the inventory of swords again to account for new one
        inventory.switchSwords = true; // Able to switch swords once inventory is updated
    }

    private void MovePlayer()
    {
        moving = false;
        if (!pickingUpSword)
        {
            if (Input.GetButton("Left"))
            {
                transform.Translate(-Vector2.right * playerSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                facingDirection = -transform.right;
                moving = true;
            }
            if (Input.GetButton("Right"))
            {
                transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
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
        if (inventory.switchSwords && swords.Count > 1) // Making sure the player has more than one sword)
        {
            if (Input.mouseScrollDelta.y > 0) // mouse scroll up
                {
                    swords[activeSwordIndex].gameObject.SetActive(false); // Disable current sword

                    // Switch to next sword
                    if (activeSwordIndex + 1 == swords.Count)
                    {
                        activeSwordIndex = 0;
                    }
                    else
                    {
                        activeSwordIndex = activeSwordIndex + 1;
                    }

                    swords[activeSwordIndex].gameObject.SetActive(true); // Re-enable the (selected) sword
                }
            else if (Input.mouseScrollDelta.y < 0) // mouse scroll down
            {
                swords[activeSwordIndex].gameObject.SetActive(false); // Disable current sword

                // Switch to next sword
                if (activeSwordIndex - 1 == -1)
                {
                    activeSwordIndex = swords.Count - 1;
                }
                else
                {
                    activeSwordIndex = activeSwordIndex - 1;
                }

                swords[activeSwordIndex].gameObject.SetActive(true); // Re-enable the (selected) sword
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) // press 1
            {
                swords[activeSwordIndex].gameObject.SetActive(false);
                swords[0].gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && swords.Count >= 2) // press 2 if player has at least 2 swords
            {
                swords[activeSwordIndex].gameObject.SetActive(false);
                swords[1].gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && swords.Count >= 3)
            {
                swords[activeSwordIndex].gameObject.SetActive(false);
                swords[2].gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && swords.Count >= 4)
            {
                swords[activeSwordIndex].gameObject.SetActive(false);
                swords[3].gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && swords.Count == 5)
            {
                swords[activeSwordIndex].gameObject.SetActive(false);
                swords[4].gameObject.SetActive(true);
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

    private void isGrounded()
    {
        float extraHeightText = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

        grounded = raycastHit.collider != null;
    }
}
