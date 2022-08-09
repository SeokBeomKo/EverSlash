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

    public WaitForSeconds wait = new WaitForSeconds(0.2f);
    public void Excute()
    {

    }

    public void StateEnter()
    {
        enemy.enemyAnim.SetTrigger("isHit");

        enemy.material.meshRenderer.material.SetColor("_BaseColor",       Color.red);
        enemy.material.meshRenderer.material.SetColor("_1st_ShadeColor",  Color.red);
        enemy.material.meshRenderer.material.SetColor("_2nd_ShadeColor",  Color.red);
    }
    public void StateExit()
    {
        enemy.enemyAnim.ResetTrigger("isHit");

        enemy.material.meshRenderer.material.SetColor("_BaseColor",       enemy.material.origin_1);
        enemy.material.meshRenderer.material.SetColor("_1st_ShadeColor",  enemy.material.origin_2);
        enemy.material.meshRenderer.material.SetColor("_2nd_ShadeColor",  enemy.material.origin_3);
    }
}
