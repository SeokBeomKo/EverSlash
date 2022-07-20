using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPrevStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            StageManager.instance.PrevStage();
        }
    }
}
