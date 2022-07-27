using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPatternList patternData;

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
        return patternData.commonPatternDatas[Random.Range(0, patternData.commonPatternDatas.Length)];
    }
    public PatternData EliteSpawn()
    {
        return patternData.elitePatternDatas[Random.Range(0, patternData.elitePatternDatas.Length)];
    }
    public PatternData BossSpawn()
    {
        return patternData.bossPatternDatas[Random.Range(0, patternData.bossPatternDatas.Length)];
    }
}
