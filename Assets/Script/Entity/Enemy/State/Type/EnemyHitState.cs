using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    // 적의 피격 상태
    // if : 스킬 사용중인가 ?
    // true : return;
    // false : 피격 애니메이션 설정 재생, 경직 시간 후 추적 상태로 변경
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
        enemy.enemyAnim.ResetTrigger("isHit");
    }
}
