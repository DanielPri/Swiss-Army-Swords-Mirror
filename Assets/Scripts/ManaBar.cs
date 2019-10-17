using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    int index;

    SpriteRenderer[] barFilled; // Array of portions of the sword bar

    void Start() {
        barFilled = transform.Find("BarFilled").GetComponentsInChildren<SpriteRenderer>();
        maxHealth = barFilled.Length;
        index = barFilled.Length - 1;
    }

    void Update() {
        
    }

}
