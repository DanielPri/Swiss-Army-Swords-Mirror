using UnityEngine;
using System.Collections;

public class PurpleGhost : BossParent
{
    [SerializeField]
    private GameObject lightningPrefab;

    private bool attacking1, isDead, isInvulnerable;
    private Animator animator;
    private Light light;
    float hurtTimer, invulnerabilityTimer;
    float hurtDuration = 0.6F;

    BossBar hitpointBar;
    SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        light = GameObject.Find("PurpleGhostLightEmission").GetComponent<Light>();
    }

    private void Update()
    {
        FaceDirection(player.transform.position);
        HandleTimers();

        if (CheckIfInRange(7f) && !attacking1 && !isDead)
        {
            StartCoroutine(LightOff());
        }
    }

    IEnumerator SpawnLightningShock()
    {
        attacking1 = true;
        yield return new WaitForSeconds(7f);
        GameObject a = Instantiate(lightningPrefab) as GameObject;
        a.transform.position = player.transform.position;
        attacking1 = false;
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
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Vector2 forceDirection = new Vector2(facingDirection.x, 1.0f) * 2f;
            player.GetComponent<Rigidbody2D>().AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }

    void Die()
    {
        isDead = true;
        Destroy(this);
    }

    private void HandleTimers()
    {
        if (CheckIfInRange(3))
        {
            // Follow the player
            Vector2 target = player.transform.position - transform.position;
            transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
            FaceDirection(player.transform.position);
        }
        else if(CheckIfInRange(5)) // move to height of player to accurately shoot projectiles
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
            hitpointBar.DecreaseBossHitpoint(2);
            isInvulnerable = true;
        }
    }

    public void Hurt()
    {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        sr.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }
}
