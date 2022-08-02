using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum EnemyType
{
    Normal,     // 일반형 몬스터
    Dash,       // 돌진형 몬스터
    Bomb,       // 자폭형 몬스터
    Smash,      // 강공형 몬스터
    Area,       // 범위형 몬스터
    Shoot       // 발사형 몬스터
}

[Serializable] public struct EnemyInfo
{
    public string name;     // 이름
    public int hp;          // 체력
    public int att;         // 공격력
    public int ignore;      // 방어 관통
    public int def;         // 방어력
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

[Serializable] public struct AreaInfo
{
    public int temp;
}

[Serializable] public struct ShootInfo
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
    public AreaInfo areaInfo;
    public ShootInfo shootInfo;
}
