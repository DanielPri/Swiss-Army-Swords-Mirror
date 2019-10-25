using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInventory : MonoBehaviour
{
    [SerializeField]
    GameObject InventoryPrefab = null;
    [SerializeField]
    GameObject RegularSwordPrefab = null;
    [SerializeField]
    GameObject FlameSwordPrefab = null;
    [SerializeField]
    GameObject IceSwordPrefab = null;
    [SerializeField]
    GameObject BrickSwordPrefab = null;
    [SerializeField]
    GameObject LightSwordPrefab = null;
    [SerializeField]
    GameObject GuitarSwordPrefab = null;

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
            ShowSword(inventoryList.Count, position);
        }
        else { // Add new
            float newPositionX = position.x + inventoryDistance * inventoryList.Count;
            newPosition = new Vector3(newPositionX, position.y, position.z);
            ShowSword(inventoryList.Count, newPosition);
        }
        GameObject slot = Instantiate(InventoryPrefab, newPosition, Quaternion.identity) as GameObject;
        inventoryList.Add(slot);
    }

    private void ShowSword(int number, Vector3 pos) {
        // We have to find the order of the swords we are getting
        GameObject swordPrefab = null;
        if (number == 0)
            swordPrefab = RegularSwordPrefab;
        if (number == 1) // Fire sword
            swordPrefab = FlameSwordPrefab;
        if (number == 2) // Brick Sword
            swordPrefab = BrickSwordPrefab;
        if (number == 3) // Ice sword
            swordPrefab = IceSwordPrefab;
        if (number == 4) // Light sword
            swordPrefab = LightSwordPrefab;
        if (number == 5) // Guitar sword
            swordPrefab = GuitarSwordPrefab;
        Instantiate(swordPrefab, pos, Quaternion.identity);
    }

    /* Controlling UI of the inventory */
    private void ControlInventory() {
        if (Input.GetButtonDown("SwordTab") && !open) { // Opening Tab
            open = true;
            inventoryList[index].GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetButtonUp("SwordTab") && open) { // Closing tab and green color to show sword equiped
            open = false;
            Color color = new Color(0.368F, 0.96F, 0.13F);
            inventoryList[index].GetComponent<SpriteRenderer>().color = color;
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
