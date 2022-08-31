using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPlayer : Player
{
    public override void AttackDelay()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["DodgeState"]);
        }

        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Debug.Log("어택 준비중");
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    public override void Attack()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("AttackDelay"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["AttackDelayState"]);
        }
    }

    public override void Dodge()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Dodge") &&
            playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }

    public override void Idle()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["DodgeState"]);
        }

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
        Debug.Log("이동 중");
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("회피 누름");
            stateMachine.ChangeState(stateMachine.stateDic["DodgeState"]);
        }

        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        if (0 == hAxis && 0 == vAxis)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }

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
