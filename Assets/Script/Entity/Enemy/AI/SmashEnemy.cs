using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashEnemy : Enemy
{
    public int skillCondition;

    public override void OnEnable() 
    {
        StartCoroutine(stateMachine.StartState());
        curHp = enemyData.enemyInfo.hp;
        
        skillCondition = 0;
    }
    public override void Trace()
    {
        // 플레이어 추격
        nav.SetDestination(target.position);

        // 플레이어가 공격범위 내에 있는가?
        if (enemyData.enemyInfo.distance >= Vector3.Distance(transform.position,target.position))
        {
            // 있다면 대기 상태로 변경
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
        // 없다면 추격 진행
    }
    public override void Idle()
    {
        // 플레이어가 공격 인식 범위에서 벗어났을 경우 추격상태로 변환
        if (enemyData.enemyInfo.distance < Vector3.Distance(transform.position,target.position))
        {
            stateMachine.ChangeState(stateMachine.stateDic["TraceState"]);
        }
        // 플레이어가 공격 인식 범위 내에 있고 공격 딜레이가 충족되었을 경우
        if (attackDelay >= enemyData.enemyInfo.attackDelay)
        {
            // 스킬 조건 만족 시 스킬상태로 변환
            if (skillCondition == enemyData.smashInfo.skillCondition)
            {
                skillCondition = 0;
                stateMachine.ChangeState(stateMachine.stateDic["SkillState"]);
            }
            // 스킬 조건 불만족 시 공격상태로 변환
            else
            {
                skillCondition++;
                stateMachine.ChangeState(stateMachine.stateDic["AttackState"]);
            }
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
    public override void Skill()
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    public void OnAttack()
    {
        RaycastHit rayHits;
        if (Physics.SphereCast(transform.position, 
                            enemyData.enemyInfo.range,
                            transform.forward,
                            out rayHits,
                            enemyData.enemyInfo.range * 0.5f,
                            LayerMask.GetMask("Player")))
        {
            StartCoroutine(rayHits.transform.GetComponent<Player>().OnHit(enemyData.enemyInfo.attack,enemyData.enemyInfo.ignore));
        }
    }
    public void OnSkill()
    {
        RaycastHit rayHits;
        if (Physics.SphereCast(transform.position, 
                            enemyData.smashInfo.skillRange,
                            transform.forward,
                            out rayHits,
                            enemyData.smashInfo.skillRange * 0.5f,
                            LayerMask.GetMask("Player")))
        {
            StartCoroutine(rayHits.transform.GetComponent<PlayerMovement>().OnDamage(enemyData.smashInfo.skillAttack,rayHits.transform.GetComponent<PlayerMovement>().defence));
        }
    }
}
