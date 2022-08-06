using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState curEnemyState;
    public void ChangeState(EnemyState state, Enemy enemy)
    {
        curEnemyState.StateExit(enemy);
        curEnemyState = state;
        curEnemyState.StateEnter(enemy);
    }
}
