using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{

    Animator anim;

    [SerializeField] List<GameObject> Doors;
    [SerializeField] float timeDelay = 0.5f;
    float timeSinceLastToggle = 0.5f;

   public void toggle() { 
        if(timeSinceLastToggle > timeDelay)
            foreach (GameObject Door in Doors) {
                //TODO add door toggle script
                timeSinceLastToggle = 0;
                Door.GetComponent<Door>().toggle();
                Debug.Log("YOU'RE IN MY LAYER, uhhh..... lever.");
                anim.SetTrigger("toggle");
            }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timeSinceLastToggle += Time.deltaTime;
    }
}
