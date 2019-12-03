using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointBar : MonoBehaviour {
    [SerializeField]
    int maxHealth;
    int index;

    float nextTimer = 0.0F;
    float timer = 1.5F;

    SpriteRenderer[] barFilled; // Array of portions of the sword bar

    void Start() {
        barFilled = transform.Find("BarFilled").GetComponentsInChildren<SpriteRenderer>();
        maxHealth = barFilled.Length;
        index = barFilled.Length - 1;
    }

    public void IncreaseHitpoint(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxHealth) {
                barFilled[index].enabled = true;
                index++;
            }
        }
    }

    public void DecreaseHitpoint(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) {
                barFilled[index].enabled = false;
                index--;
            }
        }
    }

    public int PlayerHealth
    {
        get { return index; }
    }
      

}
