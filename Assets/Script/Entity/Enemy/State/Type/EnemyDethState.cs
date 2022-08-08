using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDethState : EnemyState
{
    // 적의 죽음 상태
    // do : disable 로 전환

    public EnemyStateMachine stateMachine { get; set; }

    public void Init(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void Excute(NormalEnemy enemy)
    {

    }
    public void Excute(DashEnemy enemy)
    {

    }
    public void Excute(SmashEnemy enemy)
    {

    }
    public void Excute(BombEnemy enemy)
    {
        
    }

    public void StateEnter(Enemy enemy)
    {
        // TODO : 파편 휘날리기
    }
    public void StateExit(Enemy enemy)
    {
        
    }
}
