using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{

    [SerializeField]
    List<GameObject> Doors;

   public void toggle(Collider2D col) { 
            foreach (GameObject Door in Doors) {
                //TODO add door toggle script
                Door.GetComponent<Door>().toggle();
                Debug.Log("YOU'RE IN MY LAYER, uhhh..... lever.");
            }
    }
}
