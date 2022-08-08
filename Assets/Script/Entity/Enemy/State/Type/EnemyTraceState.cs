using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceState : EnemyState
{
    // 적의 주격 상태
    // if : 플레이어가 공격범위 내에 있는가?
    // true : 대기 상태로 변경
    // false : 추적 상태 유지
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void Excute(NormalEnemy enemy)
    {
        enemy.nav.SetDestination(enemy.target.position);
        if (enemy.enemyInfo.distance >= Vector3.Distance(enemy.transform.position,enemy.target.transform.position))
        {
            stateMachine.ChangeState(stateMachine.idleState,enemy);
        }
    }
    public void Excute(DashEnemy enemy)
    {

    }
    public void Excute(SmashEnemy enemy)
    {

    }
    public void Excute(BombEnemy enemy)
    {
        
    }

    public void StateEnter(Enemy enemy)
    {
        enemy.enemyAnim.SetBool("isTrace",true);
    }
    public void StateExit(Enemy enemy)
    {
        enemy.enemyAnim.SetBool("isTrace",false);
    }
}
