using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool doorClosed = true;
    Collider2D doorCollider;
    Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        changeDoorPhysics();
    }



    public void toggle() {

        doorClosed = !doorClosed;
        changeDoorPhysics();
    }

    void changeDoorPhysics() {
        if (doorClosed)
        {
            doorCollider.enabled = true;
            animator.SetBool("doorClosed", true);
        }
        else
        {
            doorCollider.enabled = false;
            animator.SetBool("doorClosed", false);
        }
    }

}
