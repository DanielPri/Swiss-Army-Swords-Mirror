using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    LayerMask platformLayerMask;
    [SerializeField]
    GameObject boss;
    BoxCollider2D lampCollider;

    bool canDoThings = true;

    // Start is called before the first frame update
    void Start()
    {
        lampCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() && canDoThings)
        {
            StartCoroutine(spawnBoss());
            canDoThings = false;
        }
    }

    bool isGrounded()
    {
        float extraHeightText = 0.4f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(lampCollider.bounds.center, lampCollider.bounds.size * 0.9f, 0f, Vector2.down, extraHeightText, platformLayerMask);

        Debug.DrawRay(lampCollider.bounds.center + new Vector3(lampCollider.bounds.extents.x, 0), Vector2.down * (lampCollider.bounds.extents.y + extraHeightText), Color.green);
        Debug.DrawRay(lampCollider.bounds.center - new Vector3(lampCollider.bounds.extents.x, 0), Vector2.down * (lampCollider.bounds.extents.y + extraHeightText), Color.green);
        Debug.DrawRay(lampCollider.bounds.center - new Vector3(lampCollider.bounds.extents.x, lampCollider.bounds.extents.y + extraHeightText), Vector2.right * (lampCollider.bounds.extents.x + extraHeightText), Color.green);

        return raycastHit.collider != null;
    }

    IEnumerator spawnBoss()
    {
        yield return new WaitForSeconds(1);
        Instantiate(boss, transform.position + new Vector3(0, 1), Quaternion.identity);
        Destroy(gameObject);
    }
}
