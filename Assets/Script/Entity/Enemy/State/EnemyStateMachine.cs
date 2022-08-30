using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Enemy enemy;
    public EnemyState curEnemyState;
    public Dictionary<string, EnemyState> stateDic = new Dictionary<string, EnemyState>();

    private void Awake()
    {
        stateDic.Add("AttackState" , new EnemyAttackState());
        stateDic.Add("DeathState"  , new EnemyDeathState() );
        stateDic.Add("HitState"    , new EnemyHitState()   );
        stateDic.Add("IdleState"   , new EnemyIdleState()  );
        stateDic.Add("SkillState"  , new EnemySkillState() );
        stateDic.Add("TraceState"  , new EnemyTraceState() );

        foreach(EnemyState Value in stateDic.Values)
        {
            Value.Init(this);
        }
    }

    public IEnumerator StartState()
    {
        yield return new WaitForSeconds(0.1f);
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
