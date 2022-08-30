using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] classList;
    GameObject player;

    private void Awake() {
        DataManager.instance.LoadData();
        player = Instantiate(classList[DataManager.instance.nowPlayer.playerClass]);

        GameManager.instance.player = player.transform.GetChild(0).gameObject;
    }
}
