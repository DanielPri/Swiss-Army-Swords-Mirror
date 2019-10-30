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
        
    }

    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
