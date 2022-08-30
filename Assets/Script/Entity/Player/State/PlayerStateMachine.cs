using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Player player;
    public PlayerState curPlayerState;
    public Dictionary<string, PlayerState> stateDic = new Dictionary<string, PlayerState>();

    public void ChangeState(PlayerState state)
    {
        curPlayerState.OnStateExit();
        curPlayerState = state;
        curPlayerState.OnStateEnter();
    }
}
