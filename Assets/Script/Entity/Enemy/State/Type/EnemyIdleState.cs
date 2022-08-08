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
    public float delay;

    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void Excute(NormalEnemy enemy)
    {
        if (enemy.enemyInfo.distance < Vector3.Distance(enemy.transform.position,enemy.target.transform.position))
            stateMachine.ChangeState(stateMachine.traceState,enemy);
        delay += Time.deltaTime;
        if (delay >= enemy.enemyInfo.attackDelay)
            stateMachine.ChangeState(stateMachine.attackState,enemy);
        
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
        enemy.enemyAnim.SetBool("isIdle",true);
    }
    public void StateExit(Enemy enemy)
    {
        delay = 0;
        enemy.enemyAnim.SetBool("isIdle",false);
    }
}
