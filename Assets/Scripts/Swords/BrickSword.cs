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
        // Spawn as brick wall prefab in front of brick sowrd of the player.
        GameObject playerPos = GameObject.FindGameObjectWithTag("Player");
        float dist = 15;
        Vector3 dir = Vector3.down;
        Vector3 offSet = transform.position + new Vector3(2,0,0);
        Ray ray = new Ray(offSet, dir * dist);
        RaycastHit2D rayHit = Physics2D.Raycast(offSet, dir * dist);
        Debug.DrawLine(offSet, dir * dist, Color.blue, 3.0f);
        if (rayHit.collider)
        {
            Instantiate(brickWallPrefab, new Vector3(rayHit.collider.transform.position.x, rayHit.transform.position.y, playerPos.transform.position.z), Quaternion.identity);
        }
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
