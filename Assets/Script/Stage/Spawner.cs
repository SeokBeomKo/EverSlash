using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatternType
{
    common,
    elite,
    boss
}

public class Spawner : MonoBehaviour
{
    PatternType patternType;

    [SerializeField] Transform target;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] SpawnList spawnList;

    public int[] spawnSeconds = new int[]{1,3,5,7,9};
    public int commonIndex = 0;
    public int eliteIndex = 0;
    public int bossIndex = 0;


    private void Start() {
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
        Debug.Log(time);
        if(time == spawnSeconds[commonIndex])
        {
            Debug.Log("common1");
            patternType = PatternType.common;
            SpawnEnemy(SpawnManager.instance.CommonSpawn());
            Debug.Log("common2");
            commonIndex++;
        }
        if(time == spawnSeconds[eliteIndex])
        {
            patternType = PatternType.elite;
            SpawnEnemy(SpawnManager.instance.EliteSpawn());

            eliteIndex++;
        }
        if(time == spawnSeconds[bossIndex])
        {
            patternType = PatternType.boss;
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
            switch(patternType)
            {
                case PatternType.common:
                    Debug.Log("일반 생성");
                    //int temp = Random.Range(0,spawnList.commonDatas.Length);
                    //GameObject Enemy = ObjectPooler.SpawnFromPool(spawnList.commonDatas[temp],SetPos());
                    break;
                case PatternType.elite:
                    Debug.Log("엘리트 생성");
                    break;
                case PatternType.boss:
                    Debug.Log("보스 생성");
                    break;
            }

            repeatCount--;
            if (0 == repeatCount)
                isRepeat = false;

            yield return wait;
        }
    }

    public Vector3 SetPos()
    {
        RaycastHit[] hit;                           // 레이캐스트 결과 값 저장
        Vector3 pos;                                // 적 생성 좌표 리턴값
        List<int> temp_i_list = new List<int>();    // 중복 좌표 지정 막기위한 리스트

        while(true)
        {
            int temp_i = Random.Range(0, spawnPos.Length);      // 임의의 생성 좌표 선택

            for (int i = 0; i < 1;)                             
            {
                if (temp_i_list.Contains(temp_i))               // 기존에 돌렸던 좌표와 겹치는지 검사
                {
                    temp_i = Random.Range(0, spawnPos.Length);
                }
                else
                {
                    i++;                                        // 겹치지 않았다면 다음 실행
                }
            }

            hit = Physics.RaycastAll(spawnPos[temp_i].position +    // 임의의 생성 좌표에 땅이 있는지 탐색
                                    (Vector3.up * 10f),    
                                    Vector3.down, 20f, 
                                    LayerMask.GetMask("Ground"));

            if (null != hit)                                        // 땅이 있다면 충돌한 곳의 좌표 return;
            {
                pos = hit[0].transform.position;
                return pos;
            }
            else                                                    // 땅이 없다면 해당 좌표를 중복검사 리스트에 넣고
            {                                                       // 다시 loop 를 돌린다
                temp_i_list.Add(temp_i);
            }
        }
    }
}
