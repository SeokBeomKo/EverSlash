using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum EnemyType
{
    Normal,     // 일반형 몬스터
    Dash,       // 돌진형 몬스터
    Bomb,       // 자폭형 몬스터
    Smash       // 강공형 몬스터 ( 방어력 무시 )
}

[Serializable] public struct EnemyInfo
{
    public string name;         // 이름
    public float moveSpeed;     // 이동 속도
    public int hp;              // 체력
    public float distance;      // 공격 인식범위   (공격 시작)
    public float range;         // 공격 범위       (실제 데미지유효 거리)
    public int attack;          // 공격력
    public float attackDelay;   // 공격 딜레이
    public int ignore;          // 방어 관통
    public int defence;         // 방어력
    public int exp;             // 경험치
}

[Serializable] public struct NormalInfo
{
}

[Serializable] public struct DashInfo
{
    public float skillTime;         // 스킬 대기시간
    public int skillDistance;       // 스킬 발동 조건 거리
    public float skillSpeed;        // 스킬 발동시 이동 속도
}

[Serializable] public struct BombInfo
{   
    public float skillTime;         // 스킬 대기시간
    public int skillRange;          // 스킬 범위       (실제 데미지유효 거리)
    public int skillAttack;         // 스킬 공격력
}

[Serializable] public struct SmashInfo
{
    public int skillCondition;      // 스킬 까지 남은 공격 횟수
    public int skillRange;          // 스킬 범위       (실제 데미지유효 거리)
    public int skillAttack;         // 스킬 공격력
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
