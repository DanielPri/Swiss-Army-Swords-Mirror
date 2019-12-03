using UnityEngine;
using System.Collections;

public class IceSword : Sword
{
    public GameObject iceCubePrefab;
    [SerializeField]
    GameObject freezeBall;
    float projectileDuration = 10.0F;

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

        ShootIceBall();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAbilityUsed = false;
    }

    public void ShootIceBall()
    {
        GameObject iceBall = Instantiate(freezeBall, transform.position, Quaternion.identity) as GameObject;
        FreezeBall projectileLaser = iceBall.GetComponent<FreezeBall>();
        projectileLaser.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        projectileLaser.SetDirection(player.GetFacingDirection());
        Destroy(iceBall, projectileDuration);
    }
}


