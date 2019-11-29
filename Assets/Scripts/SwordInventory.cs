using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInventory : MonoBehaviour {
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

    AudioSource inventoryToggleSound;
    AudioSource equipSound;
    AudioSource selectSound;

    public List<GameObject> inventoryList = new List<GameObject>(); // Keep track of the inventory
    public int index;
    public bool switchSwords;
    float inventoryDistance = 1.75f;

    void Start() {
        AddSlot(0);
        index = 0;
        // Audio
        AudioSource[] audioSources = GetComponents<AudioSource>();
        selectSound = audioSources[0];
        Color color = new Color(0.368F, 0.96F, 0.13F);
        inventoryList[0].GetComponent<SpriteRenderer>().color = color;
        switchSwords = true;
    }

    void Update() {
        ControlInventory();
    }

    /* Handles the addition of inventory once new sword obtained */
    public void AddSlot(int swordNumber) {
        Vector2 position = new Vector2(-7.75f, 4); // Initial position for first sword
        if (inventoryList.Count == 0) // Initialize
            ShowSword(inventoryList.Count, position);
        else { // Add new
            float newPositionX = inventoryList[inventoryList.Count - 1].transform.position.x + inventoryDistance;
            float newPositionY = inventoryList[inventoryList.Count - 1].transform.position.y;
            float newPositionZ = inventoryList[inventoryList.Count - 1].transform.position.z;
            position = new Vector3(newPositionX, newPositionY, newPositionZ);
            ShowSword(swordNumber, position);
        }
        GameObject slot = Instantiate(InventoryPrefab, position, Quaternion.identity) as GameObject;
        slot.transform.parent = gameObject.transform;
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
        var newSword = Instantiate(swordPrefab, pos, Quaternion.identity);
        newSword.transform.parent = gameObject.transform;
    }

    /* Controlling UI of the inventory */
    private void ControlInventory()
    {
        if (inventoryList.Count > 1 && switchSwords)
        {
            Color color = new Color(0.368F, 0.96F, 0.13F); // Set green color to show sword equipped
            if (Input.GetButtonDown("SwordSelection") && Input.GetAxisRaw("SwordSelection") > 0)
            {
                inventoryList[index].GetComponent<SpriteRenderer>().color = Color.white;
                index++;
                if (index == inventoryList.Count)
                    index = 0;
                inventoryList[index].GetComponent<SpriteRenderer>().color = color;
                selectSound.Play();
            }
            if (Input.GetButtonDown("SwordSelection") && Input.GetAxisRaw("SwordSelection") < 0)
            {
                inventoryList[index].GetComponent<SpriteRenderer>().color = Color.white;
                index--;
                if (index == -1)
                    index = inventoryList.Count - 1;
                inventoryList[index].GetComponent<SpriteRenderer>().color = color;
                selectSound.Play();
            }
        }
    }
}
