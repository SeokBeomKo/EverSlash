using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float delay;
    TextMeshProUGUI text;
    float time;
    bool isFade;
    private void Awake() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        isFade = false;
        time = 1f;
    }
    private void Update() {
        if (!isFade){
            time -= Time.deltaTime * delay;
            text.color = new Color(1,1,1,time);
            if (time <= 0)
                isFade = true;
        }   
        else{
            time += Time.deltaTime * delay;
            text.color = new Color(1,1,1,time);
            if (time > 1f)
                isFade = false;
        }
    
    }
}
