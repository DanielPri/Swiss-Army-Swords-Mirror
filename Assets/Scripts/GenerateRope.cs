using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateRope : MonoBehaviour
{

    [SerializeField] public
    GameObject ropeSegmentPrefab;
    
    public int qty;
    public float yOffset = 0.4375f;

    private GameObject cachedSegment;

    public void createRopesQty()
    {
        Reset();
        float offset = 0f;
        for (int i = 0; i < qty; i++)
        {
            GameObject clone = Instantiate(ropeSegmentPrefab, transform);
            clone.transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
            offset += yOffset;

            if(i > 0)
            {
                var hinge = clone.GetComponent<HingeJoint2D>();
                hinge.connectedBody = cachedSegment.GetComponent<Rigidbody2D>();
            }
            cachedSegment = clone;
        }
    }

    public void Reset()
    {
        List<Transform> childList = new List<Transform>();
        foreach (Transform child in transform)
        {
            childList.Add(child);
        }
        foreach (Transform child in childList)
        {
            DestroyImmediate(child.gameObject, true);
        }
    }
}
