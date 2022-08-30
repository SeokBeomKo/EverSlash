using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //TODO : 팩토리패턴
    PatternType patternType;

    [SerializeField] Transform      target;
    [SerializeField] Transform[]    spawnPos;
    [SerializeField] SpawnList      spawnList;

    public int commonInterval;
    public int eliteInterval;
    public int bossInterval;

    List<int> commonSeconds = new List<int>();
    List<int> eliteSeconds = new List<int>();
    List<int> bossSeconds = new List<int>();

    // int commonIndex = 0;
    // int eliteIndex  = 0;
    // int bossIndex   = 0;

    private void Start() {
        TimeManager.instance.eventPerSecond += SpawnRandomPattern;
        int temp_i_common   = commonInterval;
        int temp_i_elite    = eliteInterval;
        int temp_i_boss     = bossInterval;

        for (int i = 0; i < 1;)
        {
            if (temp_i_common < 600)
            {
                commonSeconds.Add(temp_i_common);
                temp_i_common += commonInterval;
            }
            else
            {
                commonSeconds.TrimExcess();
                eliteSeconds.TrimExcess();
                bossSeconds.TrimExcess();
                i++;
            }
            if (temp_i_elite < 600)
            {
                eliteSeconds.Add(temp_i_elite);
                temp_i_elite += eliteInterval;
            }
            if (temp_i_boss < 600)
            {
                bossSeconds.Add(temp_i_boss);
                temp_i_boss += bossInterval;
            }
        }
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
        if(commonSeconds.Contains(time))
        {
            StartCoroutine(SpawnEnemy(SpawnManager.instance.CommonSpawn()));

            commonSeconds.Remove(time);
        }
        if(eliteSeconds.Contains(time))
        {
            StartCoroutine(SpawnEnemy(SpawnManager.instance.EliteSpawn()));

            eliteSeconds.Remove(time);
        }
        if(bossSeconds.Contains(time))
        {
            StartCoroutine(SpawnEnemy(SpawnManager.instance.BossSpawn()));

            bossSeconds.Remove(time);
        }
    }

    public IEnumerator SpawnEnemy(PatternData patternData, bool isRepeat = true)
    {
        var wait = new WaitForSeconds(patternData.Delay);

        int repeatCount = patternData.Repeat;

        while(isRepeat)
        {
            switch(patternData.patternType)
            {
                case PatternType.common:
                    GameObject commonEnemy = 
                    ObjectPooler.SpawnFromPool(spawnList.commonDatas[Random.Range(0, spawnList.commonDatas.Length)],SetPos());
                    break;
                case PatternType.elite:
                    GameObject eliteEnemy = 
                    ObjectPooler.SpawnFromPool(spawnList.eliteDatas[Random.Range(0, spawnList.eliteDatas.Length)], SetPos());
                    break;
                case PatternType.boss:
                    // GameObject bossEnemy = 
                    // ObjectPooler.SpawnFromPool(spawnList.bossDatas[Random.Range(0, spawnList.bossDatas.Length)], SetPos());
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
                    temp_i = Random.Range(0, spawnPos.Length);
                else
                    i++;                                        // 겹치지 않았다면 다음 실행
            }

            hit = Physics.RaycastAll(spawnPos[temp_i].position +    // 임의의 생성 좌표에 땅이 있는지 탐색
                                    (Vector3.up * 10f),    
                                    Vector3.down, 20f, 
                                    LayerMask.GetMask("Ground"));

            if (0 != hit.Length)                                        // 땅이 있다면 충돌한 곳의 좌표 return;
            {
                pos = hit[0].point;
                return pos;
            }
            else                                                    // 땅이 없다면 해당 좌표를 중복검사 리스트에 넣고
            {                                                       // 다시 loop 를 돌린다
                temp_i_list.Add(temp_i);
            }
        }
    }
}
