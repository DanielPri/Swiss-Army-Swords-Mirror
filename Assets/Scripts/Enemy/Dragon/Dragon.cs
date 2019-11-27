using UnityEngine;
using System.Collections;

public class Dragon : Enemy
{
    /**
     *  Related to the dragon.
     */
    private Animator dragonAnimator;
    private bool isFiring;
    private bool isAttacking;

    public GameObject fireBreathPrefab;
    public float fireBreathRange = 6;


    Vector2 facingDirection;

    BossBar hitpointBar;
    BossLifeBarSpawner bossLifeBarSpawner;

    bool isHurt;
    bool isDark;

    /* Player */
    private GameObject player;
    private HitpointBar playerHPBar;
    Sword sword;

    Color c;
    SpriteRenderer sr;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isDark = false;
        player = GameObject.FindGameObjectWithTag("Player");
        dragonAnimator = GetComponent<Animator>();
        bossLifeBarSpawner = GameObject.Find("BossLifeBarSpawner").GetComponent<BossLifeBarSpawner>();
        bossLifeBarSpawner.SetDefaultPosition(transform.position + new Vector3(0, -4f, 0));
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        bossLifeBarSpawner.SetDefaultPosition(transform.position + new Vector3(0, -4f, 0));

        if (bossLifeBarSpawner.fightStart)
        {
            hitpointBar = GameObject.Find("BossLifeBar(Clone)").GetComponent<BossBar>();
            FightStart();
        }

        if (CheckIfInRange(fireBreathRange) && !isFiring)
        {
            StartCoroutine(FireBreathCoolDown());
            StartCoroutine(FireBreath());
        }

    }

    public void FightStart()
    {
        if (hitpointBar.GetHP() < 10)
        {
            TurnIntoDarkMode();
        }
    }

    public void TurnIntoDarkMode()
    {
        sr.enabled = false;
        sr.enabled = true;
        isDark = true;
        sr.color = Color.black;
    }

    public void TurnIntoNormalMode()
    {
        sr.enabled = false;
        sr.enabled = true;
        isDark = false;
        sr.color = Color.white;
    }


    IEnumerator FireBreath()
    {
        dragonAnimator.SetBool("isInRange", true);
        yield return new WaitForSeconds(1.7f);
        GameObject fb = Instantiate(fireBreathPrefab) as GameObject;
        fb.transform.position = transform.position + new Vector3(-4.5f, -0.5f, 0);
        yield return new WaitForSeconds(4f);
        Destroy(fb);
        dragonAnimator.SetBool("isInRange", false);
    }

    IEnumerator FireBreathCoolDown()
    {
        isFiring = true;
        yield return new WaitForSeconds(4f);
        isFiring = false;
        dragonAnimator.SetBool("isInRange", false);
    }

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Sword") && !isDark)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(2);
        }

        if (col.tag.Equals("Laser") && isDark)
        {
            TurnIntoNormalMode();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Sword") && !isDark)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            playerHPBar.DecreaseHitpoint(1);

            // Boss knocks back player upon collision
            Vector2 forceDirection = new Vector2(facingDirection.x, 1.0f) * 2f;
            Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();
            playerRigidBody.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }

    public void Hurt()
    {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        sr.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }
}
