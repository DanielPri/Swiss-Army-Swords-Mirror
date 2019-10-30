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

    public override void PrimaryAttack()
    {
        base.PrimaryAttack();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Enemy" && IsAttacking) // Maybe check HP of monsters...
        {
            Vector3 currentPos = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
            Instantiate(iceCubePrefab, currentPos, Quaternion.identity);
        }
    }
}
