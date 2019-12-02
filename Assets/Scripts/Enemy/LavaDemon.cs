using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDemon : BossParent
{
    [SerializeField]
    private GameObject lavaHand;

    private bool attacking1, attacking2;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FaceDirection(player.transform.position);

        if (CheckIfInRange(7f) && !attacking1)
        {
            StartCoroutine(SpawnLavaHand());
        }

        if (CheckIfInRange(3f) && !attacking2)
        {
            StartCoroutine(RamPlayer());
        }
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

    bool CheckIfInRange(float range)
    {
        return (Vector2.Distance(player.transform.position, transform.position) <= range);
    }
}
