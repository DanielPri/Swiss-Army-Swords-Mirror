using UnityEngine;
using System.Collections;

public class BrickSword : Sword
{
    public GameObject brickWallPrefab;

    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.BRICK;
    }

    public override void PrimaryAttack()
    {
        base.PrimaryAttack();
    }

    public override void SecondaryAttack()
    {
        base.SecondaryAttack();
        float dist = 1.5f;
        Vector3 dir = new Vector3(0, -1, 0);
        Vector3 offSet = transform.position + new Vector3(2, 0, 0);

        //edit: to draw ray also//
        RaycastHit2D rayHit = Physics2D.Raycast(offSet, dir * dist);
        Debug.DrawRay(offSet, dir * dist, Color.green);

        if (rayHit.collider)
        {
            Instantiate(brickWallPrefab, new Vector2(rayHit.point.x, rayHit.point.y - 0.5f), Quaternion.identity);
        }
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
