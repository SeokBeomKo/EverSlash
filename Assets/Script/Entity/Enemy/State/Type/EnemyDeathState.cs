using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    // 적의 죽음 상태
    // do : disable 로 전환
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public void Excute()
    {

    }

    public void OnStateEnter()
    {
        enemy.Death();
    }
    public void OnStateExit()
    {
        
    }
}
