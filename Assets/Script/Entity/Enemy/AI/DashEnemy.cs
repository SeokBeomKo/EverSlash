using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    public DashInfo dashInfo;
    private Vector3 temp_Direction;         // 스킬 사용 방향
    private Vector3 temp_Position;          // 스킬 사용 시작 시 포지션
    private float temp_skillDelay;
    
    public override void OnEnable() 
    {
        StartCoroutine(stateMachine.StartState());
        curHp = enemyData.enemyInfo.hp;

        temp_skillDelay = enemyData.dashInfo.skillDelay;
    }
    
    public override void Trace()
    {
        // 플레이어 추격
        nav.SetDestination(target.position);

        // 스킬 쿨 타임을 충족하는가?
        if (temp_skillDelay >= enemyData.dashInfo.skillDelay)
        {
            // 맞다면 스킬 사용
            temp_skillDelay = 0f;
            temp_Direction = target.position - transform.position;
            temp_Position = transform.position;
            stateMachine.ChangeState(stateMachine.stateDic["SkillState"]);
        }

        temp_skillDelay += Time.fixedDeltaTime;

        // 플레이어가 공격범위 내에 있는가?
        if (enemyData.enemyInfo.distance >= Vector3.Distance(transform.position,target.position))
        {
            // 있다면 대기 상태로 변경
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    public override void Idle()
    {
        // 스킬 쿨 타임을 충족하는가?
        if (temp_skillDelay >= enemyData.dashInfo.skillDelay)
        {
            // 맞다면 스킬 사용
            temp_skillDelay = 0f;
            temp_Direction = target.position - transform.position;
            temp_Position = transform.position;
            stateMachine.ChangeState(stateMachine.stateDic["SkillState"]);
        }

        temp_skillDelay += Time.fixedDeltaTime;

        // 플레이어가 공격 인식 범위에서 벗어났을 경우 추격상태로 변환
        if (enemyData.enemyInfo.distance < Vector3.Distance(transform.position,target.position))
        {
            stateMachine.ChangeState(stateMachine.stateDic["TraceState"]);
        }

        // 플레이어가 공격 인식 범위 내에 있고 공격 딜레이가 충족되었을 경우 공격상태로 변환
        if (attackDelay >= enemyData.enemyInfo.attackDelay)
        {
            stateMachine.ChangeState(stateMachine.stateDic["AttackState"]);
        }
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
        // TODO : 스킬 사용 돌진 중 충돌체? 무엇으로할지
        nav.isStopped = false;
        nav.SetDestination(transform.position + temp_Direction);
        nav.speed = enemyData.dashInfo.skillSpeed;

        // 돌진 스킬 사용 후 일정 거리에 도달했을 경우 대기상태로 변환
        if (enemyData.dashInfo.skillDistance <= Vector3.Distance(transform.position,temp_Position))
        {
            nav.isStopped = true;
            nav.speed = enemyData.enemyInfo.moveSpeed;

            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
}
