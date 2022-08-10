using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceState : EnemyState
{
    // 적의 주격 상태
    // if : 플레이어가 공격범위 내에 있는가?
    // true : 대기 상태로 변경
    // false : 추적 상태 유지
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public void Excute()
    {
        enemy.Trace();
    }

    public void OnStateEnter()
    {
        enemy.enemyAnim.SetBool("isTrace",true);
    }
    public void OnStateExit()
    {
        enemy.enemyAnim.SetBool("isTrace",false);
    }
}
