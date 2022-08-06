using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDethState : EnemyState
{
    // 적의 죽음 상태
    // do : disable 로 전환
    public void Excute(NormalEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(DashEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(SmashEnemy enemy, EnemyStateMachine stateMachine)
    {

    }
    public void Excute(BombEnemy enemy, EnemyStateMachine stateMachine)
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
