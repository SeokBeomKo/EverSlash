using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    // 적의 공격 상태
    // do : 공격 후 대기 상태로 변환
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public void Excute()
    {
        enemy.Attack();
    }

    public void StateEnter()
    {
        enemy.enemyAnim.SetTrigger("isAttack");
    }
    public void StateExit()
    {
        enemy.enemyAnim.ResetTrigger("isAttack");
    }
}
