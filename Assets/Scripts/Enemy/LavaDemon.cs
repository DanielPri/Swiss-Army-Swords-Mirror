using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDemon : BossParent
{
    [SerializeField]
    private GameObject lavaHand;
    private GameObject door; 

    private bool attacking1, attacking2, isDead;
    private Animator animator;

    BossBar hitpointBar;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        hitpointBar = GameObject.Find("BossLifeBar(Clone)").GetComponent<BossBar>();
        //door = GameObject.Find("Door");
        animator = GetComponent<Animator>();
    }

    private bool isSpawning()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("InitialState") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Spawning"))
        {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    new void Update()
    {
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();

        FaceDirection(player.transform.position);

        if (CheckIfInRange(7f) && !attacking1 && !isDead)
        {
            StartCoroutine(SpawnLavaHand());
        }

        if (CheckIfInRange(3f) && !attacking2)
        {
            StartCoroutine(RamPlayer());
        }

        HandleTimers();
    }

    IEnumerator SpawnLavaHand()
    {
        attacking1 = true;
        yield return new WaitForSeconds(3f);
        GameObject a = Instantiate(lavaHand) as GameObject;
        a.transform.position = player.transform.position;
        attacking1 = false;
    }

    IEnumerator RamPlayer()
    {
        attacking2 = true;
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f);
        attacking2 = false;
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Sword") && sword.damaging)
        {
            isHurt = true;
            hitpointBar.DecreaseBossHitpoint(1);
        }
    }

    private void HandleTimers()
    {
        if (!isSpawning())
        {
            if (CheckIfInRange(3))
            {
                // Follow the player
                Vector2 target = player.transform.position - transform.position;
                transform.Translate(target.normalized * speed * Time.deltaTime, Space.World);
            }

            if (hitpointBar.GetHP() < 1)
                Die();
        }
    }

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }

    public void Hurt()
    {
        Color firstColor = new Color(1F, 0F, 0F, 0.7F);
        Color secondColor = new Color(1F, 1F, 1F, 1F);
        sr.color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * 5.0F, 1.0F));
    }

    public new void Die()
    {
        isDead = true;
        hitpointBar.index = -1;
        animator.SetBool("isDead", true);
        //door.SetActive(true);
    }
}
