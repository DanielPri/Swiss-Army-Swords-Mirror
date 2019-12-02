using UnityEngine;
using System.Collections;

public class BossParent : Enemy
{
    /* Player */
    protected GameObject player;
    Transform playerPosition;
    protected Vector2 facingDirection;

    protected HitpointBar playerHPBar;
    protected Sword sword;


    public new virtual void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
    }

    protected virtual void FaceDirection(Vector3 playerPosition)
    {
        // Opposite to the player
        if (transform.position.x < playerPosition.x)
        {
            Vector2 newScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            playerPosition = transform.right;
        }
        else
        {
            Vector2 newScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            transform.localScale = newScale;
            facingDirection = -transform.right;
        }
    }

    protected virtual Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
}
