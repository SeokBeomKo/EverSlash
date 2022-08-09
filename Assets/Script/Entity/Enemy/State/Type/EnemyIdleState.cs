using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    // 적의 대기 상태
    // if : 적이 공격 범위 안에 있는가 ?
    // true : if : 공격 딜레이가 만족되었는가 ?
        // true : if : 스킬 조건이 만족되었는가 ?
            // true : 스킬 상태로 변환
            // false : 공격 상태로 변환
        // false : 대기 상태 유지
    // false : 추격 상태로 변환
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public void Excute()
    {
        enemy.Idle();
    }

    public void StateEnter()
    {
        enemy.enemyInfo.attackDelay = enemy.enemyData.enemyInfo.attackDelay;
        enemy.enemyAnim.SetBool("isIdle",true);
    }
    public void StateExit()
    {
        enemy.enemyAnim.SetBool("isIdle",false);
    }
}
