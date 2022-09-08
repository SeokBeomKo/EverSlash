using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TempMaterial
{
    public Color temp_c_1;
    public Color temp_c_2;
    public Color temp_c_3;
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

        temp_time = 0;
        temp_lerp = 0;
        tempMaterial.temp_c_1 = material.origin_1;
        tempMaterial.temp_c_2 = material.origin_2;
        tempMaterial.temp_c_3 = material.origin_3;
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
        stateMachine.ChangeState(stateMachine.stateDic["SkillState"]);
    }
    public override void Attack()
    {

    }
    public override void Skill()
    {
        // 지정된 쿨 동안 공격 준비
        temp_time += Time.fixedDeltaTime / enemyData.bombInfo.skillDelay;
        temp_lerp = Mathf.Lerp(0,1,temp_time);

        // 색상 변환
        tempMaterial.temp_c_1 = Color.Lerp(material.origin_1,Color.red,temp_lerp);
        material.meshRenderer.material.SetColor("_BaseColor",       tempMaterial.temp_c_1);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  tempMaterial.temp_c_1);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  tempMaterial.temp_c_1);

        if (temp_lerp >= 0.99f)
        {
            material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
            material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
            material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);
            OnSkill();
        }
    }
    public override void Death()
    {
        enemyObj.SetActive(false);

        enemyDbris.reactVec = Vector3.zero;
        enemyDbris.gameObject.SetActive(true);
    }

    public void OnSkill()
    {
        RaycastHit rayHits;
        if (Physics.SphereCast(transform.position, 
                            enemyData.bombInfo.skillRange,
                            transform.forward,
                            out rayHits,
                            enemyData.bombInfo.skillRange * 0.5f,
                            LayerMask.GetMask("Player")))
        {
            StartCoroutine(rayHits.transform.GetComponent<Player>().OnHit(enemyData.bombInfo.skillAttack,enemyData.enemyInfo.ignore));
        }
        stateMachine.ChangeState(stateMachine.stateDic["DeathState"]);
    }
}
