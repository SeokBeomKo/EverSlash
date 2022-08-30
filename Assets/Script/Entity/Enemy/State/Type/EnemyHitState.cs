using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    // 적의 피격 상태
    // if : 체력이 1 이상인가?
    // true : if : 스킬 사용중인가 ?
        // true : return;
        // false : 경직 시간 후 추격 상태로 변환
    // false : 죽음 상태로 변환
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }

    public void Excute()
    {
        enemy.Hit();
    }

    public void OnStateEnter()
    {
        enemy.enemyAnim.SetTrigger("isHit");
    }
    public void OnStateExit()
    {
        enemy.enemyAnim.ResetTrigger("isHit");
    }
}
