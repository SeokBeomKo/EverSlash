using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            SceneManager.LoadScene("04_00.Tutorial");
        }
    }
}
