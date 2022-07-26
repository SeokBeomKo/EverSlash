using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform[] spawnPos;

    private void Start() {
    }

    private void Update() {
        if (target == null)
            target = GameManager.instance.player.transform;
        
        gameObject.transform.position = target.position;
    }
}
