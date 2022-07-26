using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] classList;
    GameObject player;

    private void Start() {
        DataManager.instance.LoadData();
        player = Instantiate(classList[DataManager.instance.nowPlayer.playerClass]);
    }
}
