using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName ="Data/Spawn")]
public class SpawnList : ScriptableObject
{
    public string[] commonDatas;
    public string[] eliteDatas;
    public string[] bossDatas;

}
