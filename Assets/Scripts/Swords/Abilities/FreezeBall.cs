using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBall : Projectile
{
    public override void Awake()
    {
        base.Awake();
    }


    public override void SetDirection(Vector2 direction)
    {
        base.SetDirection(direction);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Freezing is handled in the specific enemy
        }
    }
}
