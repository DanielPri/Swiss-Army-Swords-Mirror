using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour
{
    [SerializeField]
    int maxBossHitpoint;
    int index;

    float nextTimer = 0.0F;
    float timer = 2.0F;

    SpriteRenderer[] barFilled; // Array of portions of the sword bar

    void Start() {
        barFilled = transform.Find("BarFilled").GetComponentsInChildren<SpriteRenderer>();
        maxBossHitpoint = barFilled.Length;
        index = barFilled.Length - 1;
    }

    void Update() {
    }

    public void IncreaseBossHitpoint(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index < maxBossHitpoint) { 
                barFilled[index].enabled = true;
                index++;
            }
        }
    }

    public void DecreaseBossHitpoint(int indexPosition) {
        for (int i = 0; i < indexPosition; i++) {
            if (index >= 0) { 
                barFilled[index].enabled = false;
                index--;
            }
        }
    }

}
