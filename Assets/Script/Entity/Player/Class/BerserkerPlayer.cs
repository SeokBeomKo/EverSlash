using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPlayer : Player
{
    [SerializeField] public PlayerAttackColl attColl;
    public bool isCombo;
    public Vector3 attackVec;
    public override void AttackDelay()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        attackVec = new Vector3(hAxis,0,vAxis).normalized;

        if (Input.GetButtonDown("Jump"))
        {
            if (attackVec != Vector3.zero)
                playerModel.transform.forward = attackVec;
            attackVec = Vector3.zero;
            stateMachine.ChangeState(stateMachine.stateDic["DodgeState"]);
        }

        if (Input.GetButton("Fire1"))
        {
            isCombo = true;
        }

        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("AttackDelay") &&
            playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if (isCombo)
            {
                isCombo = false;
                playerModel.transform.LookAt(transform.position + attackVec);
                if (0 == hAxis && 0 == vAxis)
                {
                    stateMachine.ChangeState(stateMachine.stateDic["AttackState"]);
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.stateDic["MobileAttackState"]);
                }
            }
            else
            {
                attackVec = Vector3.zero;
                stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
            }
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
        playerRigid.MovePosition(transform.position + playerModel.forward * (moveSpeed * 2f) * Time.deltaTime);
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Dodge") &&
            playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (0 == Input.GetAxis("Horizontal") && 0 == Input.GetAxis("Vertical"))
            {
                stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.stateDic["MoveState"]);
            }
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
        playerRigid.MovePosition(transform.position + playerModel.forward * 0.1f);
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("AttackDelay"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["AttackDelayState"]);
        }
    }
    public override void Move()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["DodgeState"]);
        }

        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        if (0 == hAxis && 0 == vAxis)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }

        if (Input.GetButton("Fire1"))
        {
            stateMachine.ChangeState(stateMachine.stateDic["MobileAttackState"]);
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

    public override void OnAttack()
    {
        attColl.damage = 10;
        attColl.ignore = 0;
        attColl.gameObject.SetActive(true);
    }
    public override void OffAttack()
    {
        attColl.gameObject.SetActive(false);
    }
}
