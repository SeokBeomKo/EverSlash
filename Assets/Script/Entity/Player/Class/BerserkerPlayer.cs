using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPlayer : Player
{
    public override void Attack()
    {
        if (true)
        {
        }
    }
    public override void Idle()
    {
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
        
    }
}
