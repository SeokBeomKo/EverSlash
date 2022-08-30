using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
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
        player.Idle();
    }

    public void OnStateEnter()
    {
        player.playerAnim.SetBool("isIdle",true);
    }
    public void OnStateExit()
    {
        player.playerAnim.SetBool("isIdle",false);
    }
}
