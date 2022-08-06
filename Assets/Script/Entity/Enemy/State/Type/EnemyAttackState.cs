using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    // 적의 공격 상태
    // if : 플레이어가 공격범위 내에 있는가?
    // true : 공격 상태 유지 및 딜레이 마다 공격
        // if : 스킬 조건이 만족 되었는가?
        // true : 스킬 상태로 변경
        // else : 공격 상태 유지
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
        enemy.enemyAnim.ResetTrigger("isAttack");
    }
}
