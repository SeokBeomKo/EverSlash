using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class Enemy : Entity, IDropExp
{
    [SerializeField] public EnemyData          enemyData;          // 적 타입에 따른 데이터 스크립터블오브젝트

    [SerializeField] public EnemyInfo           enemyInfo;          // 적 공통 데이터
    [SerializeField] public Animator            enemyAnim;          // 적 애니메이터
    [SerializeField] public Transform           target;             // 추적 대상
    [SerializeField] public NavMeshAgent        nav;                // 추적 네비매쉬
    [SerializeField] public SphereCollider      attackRange;        // 공격 인식 범위

    [SerializeField] public GameObject          enemyObj;           // 적 
    [SerializeField] public GameObject          enemyDbris;         // 적 파편

    public EnemyStateMachine stateMachine; 
      

    // 게임 시작시 설정
    private void Awake() 
    {
        enemyAnim               = GetComponent<Animator>();
        nav                     = GetComponent<NavMeshAgent>();
        material.meshRenderer   = GetComponentInChildren<SkinnedMeshRenderer>();

        material.origin_1       = material.meshRenderer.material.GetColor("_BaseColor");
        material.origin_2       = material.meshRenderer.material.GetColor("_1st_ShadeColor");
        material.origin_3       = material.meshRenderer.material.GetColor("_2nd_ShadeColor");

        // 적 공통 데이터 적용
        enemyInfo = enemyData.enemyInfo;

        // 적 이동 속도 설정
        nav.speed = enemyInfo.moveSpeed;

        // 상태패턴기계 설정
        stateMachine.enemy = this;

        // 추적 대상 설정
        StartCoroutine(Set());
    }
    IEnumerator Set()
    {
        yield return new WaitForSeconds(0.1f);
        target = GameManager.instance.player.transform;
    }

    // 오브젝트 풀링 시작시 설정
    private void OnEnable()
    {
        stateMachine.StartState();
        curHp = maxHp;
    }

    // 죽었을 시 오브젝트 풀링 리턴
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    private void FixedUpdate() 
    {
        stateMachine.curEnemyState.Excute();
    }

    abstract public void Attack();          // 공격 행동
    abstract public void Death();           // 
    abstract public void Idle();            //
    abstract public void Skill();           //
    abstract public void Trace();           //
    virtual public void Hit()
    {
        if (stateMachine)
        StartCoroutine(OnDamage(2));
    }

    public override IEnumerator OnDamage(int damage)
    {
        material.meshRenderer.material.SetColor("_BaseColor",       Color.red);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  Color.red);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  Color.red);

        yield return new WaitForSeconds(0.1f);

        if (curHp > 0){
            material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
            material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
            material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);
        }
        else{
            material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
            material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
            material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);

            // 몬스터 사망 시 파편작업
            //GameObject debris = ObjectPooler.SpawnFromPool(debrisName,transform.position);
            //debris.GetComponent<EnemyDebris>().Explosion(reactVec);
            
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "PlayerAttack")
        {
            Hit();
        }
    }
}
