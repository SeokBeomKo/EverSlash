using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyState
{
    // 적의 스킬 상태
    // do : 스킬 사용 후 대기 상태로 변환
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
        enemy.enemyAnim.SetTrigger("isSkill");
    }
    public void StateExit(Enemy enemy)
    {
        enemy.enemyAnim.ResetTrigger("isSkill");
    }
}
