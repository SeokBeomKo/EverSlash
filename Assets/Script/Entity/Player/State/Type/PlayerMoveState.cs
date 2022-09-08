using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
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
        player.Move();
    }

    public void OnStateEnter()
    {
        player.playerAnim.SetBool("isMove",true);
    }
    public void OnStateExit()
    {
        player.playerAnim.SetBool("isMove",false);
    }
}
