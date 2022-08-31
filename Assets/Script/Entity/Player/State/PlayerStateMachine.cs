using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Player player;
    public PlayerState curPlayerState;
    public Dictionary<string, PlayerState> stateDic = new Dictionary<string, PlayerState>();

    private void Awake()
    {
        stateDic.Add("AttackDelayState" , new PlayerAttackDelayState()  );
        stateDic.Add("AttackState"      , new PlayerAttackState()       );
        stateDic.Add("DeathState"       , new PlayerDeathState()        );
        stateDic.Add("DodgeState"       , new PlayerDodgeState()        );
        stateDic.Add("IdleState"        , new PlayerIdleState()         );
        stateDic.Add("MobileAttack"     , new PlayerMobileAttackState() );
        stateDic.Add("MoveState"        , new PlayerMoveState()         );
        stateDic.Add("SkillState"       , new PlayerSkillState()        );

        foreach(PlayerState Value in stateDic.Values)
        {
            Value.Init(this);
        }
    }

    public IEnumerator StartState()
    {
        yield return new WaitForSeconds(0.1f);
        stateDic.TryGetValue("IdleState", out curPlayerState);
        curPlayerState.OnStateEnter();
    }

    public void ChangeState(PlayerState state)
    {
        curPlayerState.OnStateExit();
        curPlayerState = state;
        curPlayerState.OnStateEnter();
    }
}
