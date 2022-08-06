using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyState
{
    // 적의 스킬 상태
    // if : 플레이어가 공격범위 내에 있는가?
    // true : if : 스킬 조건을 만족했는가?
        // true : 스킬 상태 유지
        // false : 공격 상태로 변경
    // false : 추적 상태로 변경
    public void Excute(NormalEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(DashEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(SmashEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(BombEnemy enemy, EnemyStateMachine stateMachine)
    {
        
    }

    public void StateEnter(Enemy enemy)
    {

    }
    public void StateExit(Enemy enemy)
    {
        
    }
}
