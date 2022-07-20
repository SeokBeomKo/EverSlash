using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextStageLobby : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            StageManager.instance.StartStage();
        }
    }
}
