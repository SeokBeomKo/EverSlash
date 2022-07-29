using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyMaterial
{
    public SkinnedMeshRenderer meshRenderer;
    public Color origin_1;
    public Color origin_2;
    public Color origin_3;
}
abstract public class Enemy : MonoBehaviour
{
    public EnemyInfo enemyInfo; 
    public Transform target;
    public int maxHp;
    public int curHp;
    public float moveSpeed;
    public float attackSpeed;
    public int attack;
    public int ignore;
    public int defence;
    public int expDrop;

    public EnemyMaterial enemtMatarial;

    private void OnEnable()
    {
        target = GameManager.instance.player.transform;
        curHp = maxHp;
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    abstract public void Attack();
    abstract public void AttackReady();
    abstract public void Tracing();
}
