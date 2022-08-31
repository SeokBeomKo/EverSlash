using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDelayState : PlayerState
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
        player.AttackDelay();
    }

    public void OnStateEnter()
    {
        
    }
    public void OnStateExit()
    {
    }
}
