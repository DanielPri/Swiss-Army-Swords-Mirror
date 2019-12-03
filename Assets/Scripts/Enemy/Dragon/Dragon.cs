using UnityEngine;
using System.Collections;

public class Dragon : BossParent
{
    /**
     *  Related to the dragon.
     */
    private Animator dragonAnimator;
    private bool onlyOnce;
    private bool isFiring;
    private bool isTailWhipping;
    private bool isAttacking;

    public GameObject fireBreathPrefab;
    public GameObject smokePrefab;
    public float fightRange = 15;
    public float fireBreathRange = 7;
    public float tailWhipRange = 4;

    float hurtTimer = 0.0F;

    BossBar hitpointBar;

    bool isDark;

    private bool knockBack;

    SpriteRenderer sr;

    private bool oneTime;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        isDark = false;
        player = GameObject.FindGameObjectWithTag("Player");
        dragonAnimator = GetComponent<Animator>();
        playerHPBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();

        if (CheckIfInRange(fightRange) && !onlyOnce)
        {
            FightStart();
        }

        if (CheckIfInRange(tailWhipRange) && !isTailWhipping)
        {
            StartCoroutine(TailWhip());
        }

        if (CheckIfInRange(fireBreathRange) && !isFiring)
        {
            StartCoroutine(FireBreath());
        }

        HandleTimers();

    }

    private void HandleTimers()
    {
        if (isHurt)
        {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= 2.0F)
            {
                isHurt = false;
                hurtTimer = 0.0f;
            }
            Hurt();
        }

        if (isDark)
        {
            TurnIntoDarkMode();
        }
        else
        {
            TurnIntoNormalMode();
        }

        FaceDirection(player.transform.position);
    }

    public void FightStart()
    {
        hitpointBar = GameObject.Find("BossLifeBar(Clone)").GetComponent<BossBar>();

        if (hitpointBar.GetHP() < 10 && !oneTime)
        {
            TurnIntoDarkMode();
            oneTime = true;
        }
    }

    public void TurnIntoDarkMode()
    {
        isDark = true;
        sr.color = Color.black;
        StartCoroutine(LerpScaleOverTime(transform.localScale, new Vector3(15, 15, 15), 3f));
        transform.parent.position = new Vector3(player.transform.position.x - 5, -0.95f, 0);
    }

    public void TurnIntoNormalMode()
    {
        sr.color = Color.white;
    }

    private IEnumerator LerpScaleOverTime(Vector3 startingScale, Vector3 endingScale, float time)
    {
        float inversedTime = 1 / time; // Compute this value **once**
        for (float step = 0.0f; step < 1.0f; step += Time.deltaTime * inversedTime)
        {
            transform.localScale = Vector3.Lerp(startingScale, endingScale, step);
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator FireBreath()
    {
        isFiring = true;
        dragonAnimator.SetBool("isInRange", true);
        yield return new WaitForSeconds(1.7f);
        GameObject fb = Instantiate(fireBreathPrefab) as GameObject;
        if (GetFacingDirection().x < 0)
        {
           fb.transform.position = new Vector3(transform.position.x-5.0f, -2.5f, 0);
        } else
        {
            fb.transform.right = -fb.transform.right;
            fb.transform.position = new Vector3(transform.position.x+5.0f, -2.5f, 0);
        }
        yield return new WaitForSeconds(1f);
        dragonAnimator.SetBool("isInRange", false);
        yield return new WaitForSeconds(6f); //Cooldown
        isFiring = false;

    }

    IEnumerator TailWhip()
    {
        isTailWhipping = true;
        dragonAnimator.SetBool("isTailWhipping", true);

        if (isTailWhipping)
        {
            isTailWhipping = !isTailWhipping;
            if(GetFacingDirection().x < 0)
            {
                player.GetComponent<Rigidbody2D>().AddForce(transform.right * -20);
                player.GetComponent<Rigidbody2D>().AddForce(transform.up * 20);
            }

        }
        yield return new WaitForSeconds(1f);
        dragonAnimator.SetBool("isTailWhipping", false);
        isTailWhipping = false;
    }

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Sword") && sword.damaging && !isDark)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(sword.damage);
        }

        if (col.tag.Equals("Laser") && isDark)
        {
            isDark = false;
            TurnIntoNormalMode();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            playerHPBar.DecreaseHitpoint(1);
            playerHurtSound();
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
