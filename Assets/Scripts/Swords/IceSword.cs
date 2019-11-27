using UnityEngine;
using System.Collections;

public class IceSword : Sword
{
    public GameObject iceCubePrefab;

    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.ICE;
    }

    public override void Attack()
    {
        base.Attack();
        isAbilityUsed = false;
    }

    public override void Ability()
    {
        base.Ability();
        swordAnimator.SetTrigger("attack");
        isAbilityUsed = true;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Enemy" && isAbilityUsed) // Maybe check HP of monsters... gotta check it's attacking too.
        {
            Mob mob = collision.gameObject.GetComponent<Mob>();
            mob.isFrozen = true;
            Vector3 currentPos = collision.gameObject.transform.position;
            GameObject prefab = Instantiate(iceCubePrefab, currentPos, Quaternion.identity);
            StartCoroutine(Unfreeze(collision.gameObject, prefab));
            mob.isFrozen = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAbilityUsed = false;
    }

    IEnumerator Unfreeze(GameObject gameObject, GameObject prefab)
    {
        Animator animator = prefab.GetComponent<Animator>();
        gameObject.SetActive(false);
        yield return new WaitForSeconds(8f);
        animator.SetBool("isUnfrozen", true);
        yield return new WaitForSeconds(2.51f);
        Destroy(prefab);
        gameObject.SetActive(true);
        
    }
}


