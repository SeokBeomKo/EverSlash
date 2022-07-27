using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatternType
{
    pattern1,
    pattern2,

}

public class SpawnManager : MonoBehaviour
{
    public SpawnPatternList spawnList;

    public static SpawnManager instance;
    private void Awake()
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion

    }



    public PatternData CommonSpawn()
    {
        return spawnList.commonPatternDatas[Random.Range(0, spawnList.commonPatternDatas.Length)];
    }
    public PatternData EliteSpawn()
    {
        return spawnList.elitePatternDatas[Random.Range(0, spawnList.elitePatternDatas.Length)];
    }
    public PatternData BossSpawn()
    {
        return spawnList.bossPatternDatas[Random.Range(0, spawnList.bossPatternDatas.Length)];
    }
}
