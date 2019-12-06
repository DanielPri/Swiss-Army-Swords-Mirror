using UnityEngine;
using System.Collections;
using System;

public class BrickSword : Sword
{
    public GameObject brickWallPrefab;
    [SerializeField]
    int maxBrickWalls = 3;
    [SerializeField]
    Sprite indicatorSprite;
    GameObject wallContainer;
    GameObject indicator;
    RaycastHit2D[] hit;

    public override void Start()
    {
        base.Start();
        base.swordType = SwordType.BRICK;
        createIndicator();
        createWallContainer();
    }

    private void createIndicator()
    {
        indicator = GameObject.Find("Indicator");
        if (indicator == null)
        {
            indicator = new GameObject("Indicator");
            indicator.transform.parent = transform;
            indicator.AddComponent<SpriteRenderer>();
            indicator.GetComponent<SpriteRenderer>().sprite = indicatorSprite;
            indicator.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            indicator.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }

    new public void Update()
    {
        base.Update();
        checkIfDestroyWall();
        updateIndicator();
    }

    private void updateIndicator()
    {
        float dist = 3f;
        Vector3 offSet;
        Vector3 facingDirection = player.GetFacingDirection();
        offSet = transform.position + facingDirection * 2;

        Vector3 dir = new Vector3(0, -1, 0);
        hit = Physics2D.RaycastAll(offSet, dir * dist);

        foreach (RaycastHit2D rayHit in hit)
        {
            if (rayHit.collider) // Check that it only hits a ground
            {
                if (rayHit.collider.tag == "Ground")
                {
                    indicator.transform.position = new Vector3(rayHit.point.x, rayHit.point.y + 0.8f);
                }
            }
        }
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
        foreach (RaycastHit2D rayHit in hit)
        {
            if (rayHit.collider) // Check that it only hits a ground
            {
                if (rayHit.collider.tag == "Ground" && player.grounded)
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
        if(wallContainer != null && wallContainer.transform.childCount > maxBrickWalls)
        {
            GameObject oldestWall;
            for (int childIndex = 0; childIndex < wallContainer.transform.childCount - maxBrickWalls; childIndex++)
            {
                oldestWall = wallContainer.transform.GetChild(childIndex).gameObject;
                Animator wallAnimator = oldestWall.GetComponentInChildren<Animator>();
                wallAnimator.SetTrigger("destroy");
                Destroy(oldestWall, 0.8f);
            }
        }
          
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
