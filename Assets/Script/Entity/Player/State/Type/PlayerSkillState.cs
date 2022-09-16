using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerState
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
        player.Skill();
    }

    public void OnStateEnter()
    {
        player.playerSkill.curSkill.Use();      // 스킬 사용
    }
    public void OnStateExit()
    {
    }
}
