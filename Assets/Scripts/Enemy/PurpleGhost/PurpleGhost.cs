using UnityEngine;
using System.Collections;

public class PurpleGhost : BossParent
{
    [SerializeField]
    private GameObject lightningPrefab;

    private bool attacking1, attacking2, isDead, isInvulnerable, toWait;
    private Light light;
    float hurtTimer, invulnerabilityTimer;
    float hurtDuration = 0.6F;
    float distanceFromPlayer;
    Rigidbody2D rg;
    HitpointBar playerBar;

    BossBar hitpointBar;
    SpriteRenderer sr;
    Music music;
    AudioSource[] audioSources;

    private new void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        light = GameObject.Find("PurpleGhostLightEmission").GetComponent<Light>();
        rg = GetComponent<Rigidbody2D>();
        playerBar = GameObject.Find("HitpointBar").GetComponent<HitpointBar>();
        music = GameObject.Find("Music").GetComponent<Music>();
        audioSources = music.GetComponents<AudioSource>();
        audioSources[2].Stop();
        transform.GetComponent<AudioSource>().Play();
        transform.GetComponent<SpriteRenderer>().flipX = true;
    }

    private new void Update()
    {
        hitpointBar = GameObject.Find("BossLifeBar(Clone)").GetComponent<BossBar>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();

        FaceDirection(player.transform.position);
        HandleTimers();

        if (CheckIfInRange(7f) && !attacking1 && !isDead)
        {
            StartCoroutine(SpawnLightningShock());
        }

        if (CheckIfInRange(17f) && !attacking2 && !isDead)
        {
            StartCoroutine(LightOff());
        }

        GetDistanceFromPlayer();

        if (playerBar.PlayerHealth <= 0)
        {
            transform.GetComponent<AudioSource>().Stop();
            music.level2 = false;
            Destroy(this);
        }
    }

    IEnumerator SpawnLightningShock()
    {
        attacking1 = true;
        GameObject a = Instantiate(lightningPrefab) as GameObject;
        a.transform.position = transform.position;
        yield return new WaitForSeconds(3f);
        attacking1 = false;
    }

    IEnumerator Wait()
    {
        toWait = true;
        yield return new WaitForSeconds(7f);
        toWait = false;
    }

    IEnumerator LightOff()
    {
        light.enabled = false;
        yield return new WaitForSeconds(6);
        light.enabled = true;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Laser"))
        {
            if (!light.enabled)
            {
                light.enabled = true;
            }
            hitpointBar.DecreaseBossHitpoint(1);
        }

        if (collision.gameObject.name == "Player")
        {
            Vector2 forceDirection = new Vector2(facingDirection.x, 1.0f);
            player.GetComponent<Rigidbody2D>().AddForce(forceDirection, ForceMode2D.Impulse);
            playerBar.DecreaseHitpoint(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    }

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }

    private void LateUpdate()
    {
        if (hitpointBar.GetHP() < 1)
            Die();
    }

    new void Die()
    {
        isDead = true;
        Destroy(this);
        gameObject.SetActive(false);
        GameObject.Find("BlockingCrate").SetActive(false);
        transform.GetComponent<AudioSource>().Stop();
        music.audioSources[2].Play();
    }

    private void GetDistanceFromPlayer()
    {
        distanceFromPlayer = (transform.position - player.transform.position).magnitude;
    }


    private void HandleTimers()
    {
        if (distanceFromPlayer > 3)
        {
            // Follow the player
            Vector2 target = player.transform.position - transform.position;
            transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
            FaceDirection(player.transform.position);
            StartCoroutine(Wait());

        }
        else // move to height of player to accurately shoot projectiles
        {
            if (transform.position.y > player.transform.position.y)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
                FaceDirection(player.transform.position);
            }
            else if (transform.position.y < player.transform.position.y)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
                FaceDirection(player.transform.position);
            }
        }
        if (isHurt)
        {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration)
            {
                isHurt = false;
                hurtTimer = 0.0f;
            }
            Hurt();
        }
        else
        {
            sr.color = new Color(1F, 1F, 1F, 1F);
        }
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > hurtDuration)
            {
                isInvulnerable = false;
                invulnerabilityTimer = 0.0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sword" && sword.damaging && !isInvulnerable)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(3);
            isInvulnerable = true;
            Vector2 forceDirection = new Vector2(facingDirection.x, 1.0f) * 2f;
            rg.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }

    public void Hurt()
    {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        sr.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }
}
