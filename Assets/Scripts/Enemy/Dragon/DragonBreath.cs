using UnityEngine;
using System.Collections;

public class DragonBreath : Fire
{
    [SerializeField]
    public Fire fireprefab;
    private Vector3 originalPos;

    public override void Initialize()
    {
        base.Initialize();
    }

    private void Start()
    {
        Initialize();
        originalPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player.playerHPBar.DecreaseHitpoint(5);
        }
    }

    private void OnDestroy()
    {
        Fire fire = Instantiate(fireprefab) as Fire;
        fire.transform.position = originalPos;
    }

}
