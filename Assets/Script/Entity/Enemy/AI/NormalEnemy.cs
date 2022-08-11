using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected NormalInfo normalInfo;
    public override void Trace()
    {
        // 플레이어 추격
        nav.SetDestination(target.position);

        // 플레이어가 공격범위 내에 있는가?
        if (enemyData.enemyInfo.distance >= Vector3.Distance(transform.position,target.transform.position))
        {
            // 있다면 대기 상태로 변경
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
        // 없다면 추격 진행
    }

    public override void Idle()
    {
        // 플레이어가 공격 인식 범위에서 벗어났을 경우 추격상태로 변환
        if (enemyData.enemyInfo.distance < Vector3.Distance(transform.position,target.transform.position))
        {
            stateMachine.ChangeState(stateMachine.stateDic["TraceState"]);
        }
        // 플레이어가 공격 인식 범위 내에 있고 공격 딜레이가 충족되었을 경우 공격상태로 변환
        if (attackDelay >= enemyData.enemyInfo.attackDelay)
        {
            stateMachine.ChangeState(stateMachine.stateDic["AttackState"]);
        }

        attackDelay += Time.fixedDeltaTime;
    }
    public override void Attack()
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }

    public void OnAttack()
    {
        Debug.Log("골렘 공격");
        RaycastHit rayHits;
        if (Physics.SphereCast(transform.position, 
                            enemyData.enemyInfo.range,
                            transform.forward,
                            out rayHits,
                            enemyData.enemyInfo.range * 0.5f,
                            LayerMask.GetMask("Player")))
        {
            int attack = Random.Range(attackMin,attackMax);
            StartCoroutine(rayHits.transform.GetComponent<PlayerMovement>().OnDamage(attack));
        }
    }
    public override void Skill()
    {
        
    }
    public override void Death()
    {
        
    }

    // override public void AttackCheck()
    // {
    //     if (enemyInfo.distance >= Vector3.Distance(target.transform.position,transform.position))
    //     {
    //         enemyState = _EnemyState.Attack;
    //     }
    //     else
    //     {
    //         enemyState = _EnemyState.Trace;
    //     }
    // }
    // override public void Tracing()
    // {
    //     if (enemyState == _EnemyState.Trace){
    //         if (target != null)
    //             nav.SetDestination(target.position);
    //     }
    // }
}
