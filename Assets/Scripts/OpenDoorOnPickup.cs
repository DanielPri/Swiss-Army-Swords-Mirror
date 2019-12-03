using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnPickup : MonoBehaviour
{
    [SerializeField] GameObject doorGO;
    Door door;
    // Start is called before the first frame update
    void Start()
    {
        door = doorGO.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            door.toggle();
    }
}
