using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPlayer : Player
{
    public override void AttackDelay()
    {
        
    }
    public override void Attack()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    public override void Idle()
    {
        if (Input.GetButton("Fire1"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["AttackState"]);
        }
        if (0 != Input.GetAxis("Horizontal") || 0 != Input.GetAxis("Vertical"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["MoveState"]);
        }
    }
    public override void MobileAttack()
    {
        
    }
    public override void Move()
    {
        if (0 == Input.GetAxis("Horizontal") && 0 == Input.GetAxis("Vertical"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }

        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        moveVec = new Vector3(hAxis,0,vAxis).normalized;

        playerModel.transform.LookAt(transform.position + moveVec);

        // 현재 위치 + 이동 방향 + 이동 속도 + 입력 시간
        playerRigid.MovePosition(transform.position + moveVec * moveSpeed * Time.deltaTime);
    }
    public override void Skill()
    {
        // TODO : 스킬 시스템
    }
}
