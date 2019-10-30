using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool doorClosed = true;
    Collider2D doorCollider;
    void Start() {
        doorCollider = GetComponent<Collider2D>();
        colliderChange();
    }
    public void toggle() {

        doorClosed = !doorClosed;

        //animateDoor
        colliderChange();


    }

    void colliderChange() {
        if (doorClosed)
        {
            doorCollider.enabled = true;
        }
        else
        {
            doorCollider.enabled = false;
        }
    }
}
