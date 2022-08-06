using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState
{
    public void Excute(NormalEnemy  enemy, EnemyStateMachine stateMachine);
    public void Excute(DashEnemy    enemy, EnemyStateMachine stateMachine);
    public void Excute(SmashEnemy   enemy, EnemyStateMachine stateMachine);
    public void Excute(BombEnemy    enemy, EnemyStateMachine stateMachine);

    public void StateEnter(Enemy enemy);
    public void StateExit(Enemy enemy);
}
