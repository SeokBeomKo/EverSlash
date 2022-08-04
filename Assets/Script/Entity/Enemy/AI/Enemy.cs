using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


abstract public class Enemy : Entity
{
    [SerializeField] public EnemyData enemyData;    // 적 타입에 따른 데이터 스크립터블오브젝트
    public EnemyInfo enemyInfo;                     // 적 공통 데이터
    public Transform target;                        // 추적 대상
    public NavMeshAgent nav;                        // 추적 네비매쉬

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
