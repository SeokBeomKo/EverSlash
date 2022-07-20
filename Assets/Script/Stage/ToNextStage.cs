using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            StageManager.instance.NextStage();
        }
    }
}
