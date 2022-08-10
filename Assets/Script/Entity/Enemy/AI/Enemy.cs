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

    abstract public void Attack();                      // 공격 행동
    abstract public void Idle();                        // 대기 행동
    abstract public void Skill();                       // 스킬 행동
    abstract public void Trace();                       // 추격 행동
    virtual public void Hit()                           // 피격 행동
    {
        // 피격 애니메이션 재생 끝났을 시 대기 상태로 변환
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    virtual public void Death()                         // 사망 행동
    {
        enemyObj.SetActive(false);
        enemyDbris.SetActive(true);
        
    }                        

    public override IEnumerator OnDamage(int _damage, int _ignore)
    {
        // 피격 데미지 처리
        int damage = _damage - (defence - ignore);
        curHp -= damage;

        // 사망 처리
        if (0 >= curHp)
        {
            stateMachine.ChangeState(stateMachine.stateDic["DeathState"]);
        }

        // 스킬 행동 아닐 경우 애니메이션 재생 처리
        else if (stateMachine.curEnemyState != stateMachine.stateDic["SkillState"])
        {
            stateMachine.ChangeState(stateMachine.stateDic["HitState"]);
        }

        material.meshRenderer.material.SetColor("_BaseColor",       Color.red);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  Color.red);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  Color.red);

        yield return new WaitForSeconds(0.1f);

        if (curHp >= 0)
        {
            material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
            material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
            material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);
        }
        else
        {
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
            var temp = other.GetComponent<AttackCollider>();
            // 피격 이펙트 처리
            StartCoroutine(OnDamage(temp.TurnDamage(),temp.TurnIgnore()));
        }
    }
}
