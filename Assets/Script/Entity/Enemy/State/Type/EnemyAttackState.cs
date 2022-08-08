using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    // 적의 공격 상태
    // do : 공격 후 대기 상태로 변환

    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void Excute(NormalEnemy enemy)
    {

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
        enemy.enemyAnim.SetTrigger("isAttack");
    }
    public void StateExit(Enemy enemy)
    {
        enemy.enemyAnim.ResetTrigger("isAttack");
    }
}
