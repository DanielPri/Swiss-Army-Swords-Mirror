using UnityEngine;
using System.Collections;

public class LightSword : Sword
{
    public GameObject lightLaserPrefab;

    public override void Start() {
        base.Start();
        base.swordType = SwordType.LIGHT;
    }

    public override void Attack() {
        base.Attack();
    }

    public override void Ability() {
        base.Ability();
    }

    public override void OnTriggerEnter2D(Collider2D collision) {

    }
}
