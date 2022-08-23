using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    public DashInfo dashInfo;
    public float temp_skillDelay;
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
    }
    public override void Death()
    {
        
    }
}
