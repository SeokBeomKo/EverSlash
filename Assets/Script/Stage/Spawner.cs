using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] SpawnList spawnList;

    public int[] spawnSeconds = new int[]{1,3,5,7,9};
    public int commonIndex = 0;
    public int eliteIndex = 0;
    public int bossIndex = 0;


    private void OnEnable() {
        TimeManager.instance.eventPerSecond += SpawnRandomPattern;
    }
    private void OnDisable() {
        TimeManager.instance.eventPerSecond -= SpawnRandomPattern;
    }

    private void Update() {
        if (target == null)
            target = GameManager.instance.player.transform;
        
        gameObject.transform.position = target.position;
    }

    public void SpawnRandomPattern(int time)
    {
        if(time == spawnSeconds[commonIndex])
        {
            SpawnEnemy(SpawnManager.instance.CommonSpawn());

            commonIndex++;
        }
        if(time == spawnSeconds[eliteIndex])
        {
            SpawnEnemy(SpawnManager.instance.EliteSpawn());

            eliteIndex++;
        }
        if(time == spawnSeconds[bossIndex])
        {
            SpawnEnemy(SpawnManager.instance.BossSpawn());

            bossIndex++;
        }
        

    }

    public IEnumerator SpawnEnemy(PatternData patternData, bool isRepeat = true)
    {
        var wait = new WaitForSeconds(patternData.Delay);
        
        int repeatCount = patternData.Repeat;

        while(isRepeat)
        {
            // TODO : 오브젝트 풀링 몬스터 소환

            repeatCount--;
            if (0 == repeatCount)
                isRepeat = false;

            yield return wait;
        }
    }

}
