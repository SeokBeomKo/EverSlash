using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyState
{
    // 적의 스킬 상태
    // do : 스킬 사용 후 대기 상태로 변환
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public void Excute()
    {
        enemy.Skill();
    }

    public void OnStateEnter()
    {
        enemy.enemyAnim.SetTrigger("isSkill");
    }
    public void OnStateExit()
    {
        enemy.enemyAnim.ResetTrigger("isSkill");
    }
}
