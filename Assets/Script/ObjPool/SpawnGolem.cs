using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGolem : MonoBehaviour
{
    public Transform spawnPoint;
    public string enemyTag;
    public int enemyCount;
    public int enemyCurCount;
    public int enemyMaxCount;
    public float spawnDelay;
    private float spawnDelayCheck;

    private void Awake() {
        spawnDelayCheck = spawnDelay;
    }
    void Update()
    {
        spawnDelayCheck -= Time.deltaTime;
        if (spawnDelayCheck <= 0f){
            for (int i = 0; i < enemyCount; i++){
                GameObject Enemy = ObjectPooler.SpawnFromPool(enemyTag,spawnPoint.position);
                enemyCurCount++;
                if (enemyCurCount >= enemyMaxCount){
                    Destroy(gameObject);
                }
            }
            spawnDelayCheck = spawnDelay;
        }
    }
}
