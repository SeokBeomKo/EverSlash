using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public struct PatternData{
    [Header("패턴 이름")]
    public string Name;
    [Header("회당 소환 개수")]
    public int EA;
    [Header("반복 횟수")]
    public int Repeat;
    [Header("반복 간격")]
    public float Delay;
}



[CreateAssetMenu(fileName = "SpawnPatternData", menuName ="Data/SpawnPattern")]
public class SpawnPatternList : ScriptableObject
{

    public PatternData[] commonPatternDatas;
    public PatternData[] elitePatternDatas;
    public PatternData[] bossPatternDatas;

}
