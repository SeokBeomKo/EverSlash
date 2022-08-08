using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState curEnemyState;

    public EnemyState idleState;
    public EnemyState traceState;
    public EnemyState attackState;
    public EnemyState skillState;
    public EnemyState hitState;
    public EnemyState dethState;

    private void Start() {
        idleState   = new EnemyIdleState();
        traceState  = new EnemyTraceState();
        attackState = new EnemyAttackState();
        skillState  = new EnemySkillState();
        hitState    = new EnemyHitState();
        dethState   = new EnemyDethState();

        idleState  .Init(this);     traceState .Init(this);     attackState.Init(this);
        skillState .Init(this);     hitState   .Init(this);     dethState  .Init(this);
    }

    public void StartState(Enemy enemy)
    {
        curEnemyState = traceState;
        curEnemyState.StateEnter(enemy);
    }
    public void ChangeState(EnemyState state, Enemy enemy)
    {
        curEnemyState.StateExit(enemy);
        curEnemyState = state;
        curEnemyState.StateEnter(enemy);
    }
}
