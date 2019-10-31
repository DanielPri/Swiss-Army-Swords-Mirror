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
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Enemy") // Maybe check HP of monsters... gotta check it's attacking too.
        {
           Mob mob = collision.gameObject.GetComponent<Mob>();                
                
            if (mob.easyMobHP < 1)
            {
                Vector3 currentPos = collision.gameObject.transform.position;
                Destroy(collision.gameObject);
                Instantiate(iceCubePrefab, currentPos, Quaternion.identity);
            }
        }
    }
}
