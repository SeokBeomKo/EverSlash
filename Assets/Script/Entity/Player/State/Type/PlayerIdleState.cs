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
        Debug.Log("아이들 스테이트 시작");
        player.playerAnim.SetBool("isIdle",true);
    }
    public void OnStateExit()
    {
        Debug.Log("아이들 스테이트 종료");
        player.playerAnim.SetBool("isIdle",false);
    }
}
