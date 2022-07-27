using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPatternList patternList;

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
        return patternList.commonPatternDatas[Random.Range(0, patternList.commonPatternDatas.Length)];
    }
    public PatternData EliteSpawn()
    {
        return patternList.elitePatternDatas[Random.Range(0, patternList.elitePatternDatas.Length)];
    }
    public PatternData BossSpawn()
    {
        return patternList.bossPatternDatas[Random.Range(0, patternList.bossPatternDatas.Length)];
    }
}
