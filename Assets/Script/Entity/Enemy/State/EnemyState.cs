using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState
{
    public Enemy enemy { get; set; }
    public EnemyStateMachine stateMachine { get; set; }
    public void Init(EnemyStateMachine stateMachine);
    public void Excute();

    public void StateEnter();
    public void StateExit();
}
