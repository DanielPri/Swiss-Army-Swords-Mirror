using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    protected Player player;

    // Use this for initialization
    public virtual void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        Initialize();
        Destroy(this.gameObject, 3.0f);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player.playerHPBar.DecreaseHitpoint(1);
        }
    }

}
