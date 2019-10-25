using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    int maxMana;
    int index;

    float nextTimer = 0.0F;
    float timer = 2.0F;

    SpriteRenderer[] barFilled; // Array of portions of the sword bar

    void Start() {
        barFilled = transform.Find("BarFilled").GetComponentsInChildren<SpriteRenderer>();
        maxMana = barFilled.Length;
        index = barFilled.Length - 1;
    }

    void Update() {
        if (Time.time > nextTimer) {
            nextTimer = Time.time + timer;
            DecreaseMana(1); // Testing the mana bar
        }
    }

    public void IncreaseMana(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxMana) { 
                barFilled[index].enabled = true;
                index++;
            }
        }
    }

    public void DecreaseMana(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) { 
                barFilled[index].enabled = false;
                index--;
            }
        }
    }

}
