using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct EnemyMaterial
{
    public SkinnedMeshRenderer meshRenderer;
    public Color origin_1;
    public Color origin_2;
    public Color origin_3;
}
abstract public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData enemyData;
    public EnemyInfo enemyInfo; 
    public Transform target;
    public int maxHp;
    public int curHp;
    public NavMeshAgent nav;
    public float moveSpeed;
    public float attackSpeed;
    public int attack;
    public int ignore;
    public int defence;
    public int expDrop;

    public EnemyMaterial enemtMatarial;

    // 게임 시작시 설정
    private void Awake() 
    {
        target = GameManager.instance.player.transform;
        nav = GetComponent<NavMeshAgent>();    
    }

    // 오브젝트 풀링 시작시 설정
    private void OnEnable()
    {
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
