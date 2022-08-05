using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected NormalInfo normalInfo;
    override public void Attack()
    {
        RaycastHit rayHits;
        if (Physics.SphereCast(transform.position, 
                            enemyInfo.range,
                            transform.forward,
                            out rayHits,
                            enemyInfo.range * 0.5f,
                            LayerMask.GetMask("Player")))
        {
            int attack = Random.Range(attackMin,attackMax);
            StartCoroutine(rayHits.transform.GetComponent<PlayerMovement>().OnDamage(attack));
        }
    }

    override public void AttackCheck()
    {
        if (enemyInfo.distance >= Vector3.Distance(target.transform.position,transform.position))
        {
            enemyState = _EnemyState.Attack;
        }
        else
        {
            enemyState = _EnemyState.Trace;
        }
    }
    override public void Tracing()
    {
        if (enemyState == _EnemyState.Trace){
            if (target != null)
                nav.SetDestination(target.position);
        }
    }
}
