using UnityEngine;
using System.Collections;
using System;

public class BrickSword : Sword
{
    public GameObject brickWallPrefab;
    [SerializeField]
    int maxBrickWalls = 3;
    GameObject wallContainer;

    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.BRICK;
        createWallContainer();
    }

    new public void Update()
    {
        base.Update();
        checkIfDestroyWall();
    }

    private void createWallContainer()
    {
        wallContainer = GameObject.Find("WallContainer");
        if(wallContainer == null)
        {
            wallContainer = new GameObject("WallContainer");
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Ability()
    {
        base.Ability();
        GenerateWallBrickOnRayCast();
    }

    private void GenerateWallBrickOnRayCast()
    {
        float dist = 3f;
        Vector3 facingDirection = player.GetFacingDirection();
        Vector3 offSet;

        if (facingDirection.x < 0)
        {
            offSet = transform.position + new Vector3(-2, 0, 0);
        }
        else
        {
            offSet = transform.position + new Vector3(2, 0, 0);
        }

        Vector3 dir = new Vector3(0, -1, 0);
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(offSet, dir * dist);

        foreach (RaycastHit2D rayHit in hit)
        {
            if (rayHit.collider) // Check that it only hits a ground
            {
                if (rayHit.collider.tag == "Ground")
                {
                    GameObject BrickWall = Instantiate(brickWallPrefab, new Vector2(rayHit.point.x, rayHit.point.y), Quaternion.identity, wallContainer.transform) as GameObject;
                    Destroy(BrickWall, 4.0F);
                    break;
                }
            }
        }
    }

    private void checkIfDestroyWall()
    {
        if(wallContainer.transform.childCount > maxBrickWalls)
        {
            GameObject oldestWall;
            for (int childIndex = 0; childIndex < wallContainer.transform.childCount - maxBrickWalls; childIndex++)
            {
                oldestWall = wallContainer.transform.GetChild(childIndex).gameObject;
                Animator wallAnimator = oldestWall.GetComponentInChildren<Animator>();
                wallAnimator.SetTrigger("destroy");
                Destroy(oldestWall, 1f);
            }
        }
          
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
