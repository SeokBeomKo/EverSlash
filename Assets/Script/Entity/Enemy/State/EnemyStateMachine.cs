using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Enemy enemy;
    public EnemyState curEnemyState;
    public Dictionary<string, EnemyState> stateDic = new Dictionary<string, EnemyState>();

    private void Start()
    {
        stateDic.Add("TraceState"  , new EnemyTraceState() );
        stateDic.Add("IdleState"   , new EnemyIdleState()  );
        stateDic.Add("AttackState" , new EnemyAttackState());
        stateDic.Add("SkillState"  , new EnemySkillState() );
        stateDic.Add("HitState"    , new EnemyHitState()   );
        stateDic.Add("DeathState"   , new EnemyDeathState() );

        foreach(EnemyState Value in stateDic.Values)
        {
            Value.Init(this);
        }
    }

    public void StartState()
    {
        stateDic.TryGetValue("TraceState", out curEnemyState);
        curEnemyState.OnStateEnter();
    }
    public void ChangeState(EnemyState state)
    {
        curEnemyState.OnStateExit();
        curEnemyState = state;
        curEnemyState.OnStateEnter();
    }
}
