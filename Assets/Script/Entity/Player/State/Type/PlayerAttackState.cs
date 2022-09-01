using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public Player player { get; set; }
    public PlayerStateMachine stateMachine { get; set; }

    public void Init(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.player = stateMachine.player;
    }
    public void Excute()
    {
        player.Attack();
    }

    public void OnStateEnter()
    {
        player.OnAttack();
        player.playerAnim.SetTrigger("isAttack");
    }
    public void OnStateExit()
    {
        player.OffAttack();
        player.playerAnim.ResetTrigger("isAttack");
    }
}
