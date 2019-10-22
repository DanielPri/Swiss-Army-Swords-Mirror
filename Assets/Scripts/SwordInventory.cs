using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInventory : MonoBehaviour
{
    [SerializeField]
    GameObject InventoryPrefab = null;

    public List<GameObject> inventoryList = new List<GameObject>(); // Keep track of the inventory
    int index; // Should be put in a state file to keep track of it
    bool added = false; // For test

    Vector3 position = new Vector3(-8.65F, 4.08F, 0F);
    bool open; // The inventory state
    float inventoryDistance = 1.7F;

    void Start() {
        AddSlot();
        index = 0;
        open = false;
    }

    void Update() {
        if (Time.time > 1 && !added) {
            AddSlot();
            AddSlot();
            added = true;
        }
        ControlInventory();
        EquipWeapon();
    }

    /* Handles the addition of inventory once new sword obtained */
    public void AddSlot() {
        Vector3 newPosition;
        if (inventoryList.Count == 0) { // Initialize
            newPosition = position;
        }
        else { // Add new
            float newPositionX = position.x + inventoryDistance * inventoryList.Count;
            newPosition = new Vector3(newPositionX, position.y, position.z);
        }
        GameObject slot = Instantiate(InventoryPrefab, newPosition, Quaternion.identity) as GameObject;
        inventoryList.Add(slot);
    }

    /* Controlling UI of the inventory */
    private void ControlInventory() {
        if (Input.GetButtonDown("SwordTab") && !open) { // Opening Tab
            open = true;
            inventoryList[index].GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetButtonUp("SwordTab") && open) { // Closing tab
            open = false;
            inventoryList[index].GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (open && Input.GetButtonDown("SwordSelection")) {
            inventoryList[index].GetComponent<SpriteRenderer>().color = Color.white;
            index++;
            if (index == inventoryList.Count)
                index = 0;
            inventoryList[index].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void EquipWeapon() {
        // Have to keep track of swords at same indices as the inventory
        // Player.GetComponent<Player>().EquipSword(index); ?
    }

}
