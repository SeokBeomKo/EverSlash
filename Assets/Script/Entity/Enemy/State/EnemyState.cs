using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState
{
    public EnemyStateMachine stateMachine { get; set; }
    public void Init(EnemyStateMachine stateMachine);
    public void Excute(NormalEnemy  enemy);
    public void Excute(DashEnemy    enemy);
    public void Excute(SmashEnemy   enemy);
    public void Excute(BombEnemy    enemy);

    public void StateEnter(Enemy    enemy);
    public void StateExit(Enemy     enemy);
}
