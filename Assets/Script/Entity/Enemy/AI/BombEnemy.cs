using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TempMaterial
{
    public Color temp_m_1;
    public Color temp_m_2;
    public Color temp_m_3;
}
public class BombEnemy : Enemy
{
    TempMaterial tempMaterial;
    public float temp_lerp;
    public float temp_time;
    public override void OnEnable() 
    {
        StartCoroutine(stateMachine.StartState());
        curHp = enemyData.enemyInfo.hp;
        temp_lerp = 0;
        temp_time = 0;

        tempMaterial.temp_m_1 = material.origin_1;
        tempMaterial.temp_m_2 = material.origin_2;
        tempMaterial.temp_m_3 = material.origin_3;
    }
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
        stateMachine.ChangeState(stateMachine.stateDic["SkillState"]);
    }
    public override void Attack()
    {

    }
    public override void Skill()
    {
        temp_time += Time.deltaTime;
        temp_lerp = Mathf.Lerp(0,enemyData.bombInfo.skillTime,temp_time);
        Debug.Log(temp_lerp);

        // tempMaterial.temp_m_1 = Color.Lerp(material.origin_1,Color.red,1);

        // material.meshRenderer.material.SetColor("_BaseColor",       tempMaterial.temp_m_1);
        // material.meshRenderer.material.SetColor("_1st_ShadeColor",  tempMaterial.temp_m_2);
        // material.meshRenderer.material.SetColor("_2nd_ShadeColor",  tempMaterial.temp_m_3);
    }
    public override void Death()
    {
        
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
