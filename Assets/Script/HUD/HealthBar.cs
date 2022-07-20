using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    private void Awake() {
        healthBar = GetComponent<Slider>();
    }

    public void SetHP(int cur){
        healthBar.value = cur;
    }

    public void SetMaxHP(int max){
        healthBar.maxValue = max;
    }
}
