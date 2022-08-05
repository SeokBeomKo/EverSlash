using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum EnemyType
{
    Normal,     // 일반형 몬스터
    Dash,       // 돌진형 몬스터
    Bomb,       // 자폭형 몬스터
    Smash       // 강공형 몬스터
}

[Serializable] public struct EnemyInfo
{
    public string name;     // 이름
    public float distance;  // 공격 인식범위
    public float range;     // 공격 범위
    public int hp;          // 체력
    public int attack;         // 공격력
    public int ignore;      // 방어 관통
    public int defence;         // 방어력
    public int exp;         // 경험치
}

[Serializable] public struct NormalInfo
{
    public int moveSpeed;
}

[Serializable] public struct DashInfo
{
    public int temp;
}

[Serializable] public struct BombInfo
{
    public int temp;
}

[Serializable] public struct SmashInfo
{
    public int temp;
}



[CreateAssetMenu(fileName = "EnemyData", menuName ="Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyType Type;
    public EnemyInfo enemyInfo;
    public NormalInfo normalInfo;
    public DashInfo dashInfo;
    public BombInfo bombInfo;
    public SmashInfo smashInfo;
}
