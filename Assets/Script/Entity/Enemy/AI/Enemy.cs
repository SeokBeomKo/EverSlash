using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum _EnemyState
{
    Trace, Attack, Hit, Skill
}

abstract public class Enemy : Entity, IDamageable, IDropExp
{
    [SerializeField] public EnemyData enemyData;                        // 적 타입에 따른 데이터 스크립터블오브젝트
    [SerializeField] protected EnemyInfo enemyInfo;                     // 적 공통 데이터
    [SerializeField] protected Animator enemyAnim;                      // 적 애니메이터
    [SerializeField] protected Transform target;                        // 추적 대상
    [SerializeField] protected NavMeshAgent nav;                        // 추적 네비매쉬
    [SerializeField] protected SphereCollider attackRange;              // 공격 인식 범위
    private _EnemyState _enemyState;                                     // 현재 상태
    protected _EnemyState enemyState {
                                    get{return _enemyState;}
                                    set{StateExit(_enemyState);
                                        _enemyState = value;
                                        StateEnter(_enemyState);}
                                    }         

    // 게임 시작시 설정
    private void Awake() 
    {
        enemyAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        material.meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        material.origin_1 = material.meshRenderer.material.GetColor("_BaseColor");
        material.origin_2 = material.meshRenderer.material.GetColor("_1st_ShadeColor");
        material.origin_3 = material.meshRenderer.material.GetColor("_2nd_ShadeColor");

        // 적 공통 데이터 적용
        enemyInfo = enemyData.enemyInfo;

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
        enemyState = _EnemyState.Trace;
        curHp = maxHp;
    }

    // 죽었을 시 오브젝트 풀링 리턴
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    private void FixedUpdate() 
    {
        Tracing();
        AttackCheck();
        SkillCheck();
    }

    abstract public void Attack();          // 공격 행동
    abstract public void AttackCheck();     // 공격 조건 체크
    abstract public void Tracing();         // 추적 행동
    virtual public void Skill(){}           // 스킬 행동
    virtual public void SkillCheck(){}      // 스킬 행동

    public void StateEnter(_EnemyState state)
    {
        switch(state)
        {
            case _EnemyState.Trace:
            enemyAnim.SetBool("isTrace",true);
            break;
            case _EnemyState.Attack:
            // Attack(); TODO : 애니메이션에 집어넣기
            enemyAnim.SetTrigger("isAttack");
            break;
            case _EnemyState.Hit:
            StartCoroutine(OnDamage());
            enemyAnim.SetTrigger("isHit");
            break;
            case _EnemyState.Skill:
            Skill();
            enemyAnim.SetTrigger("isSkill");
            break;
            default:
            break;
        }
    }
    public void StateExit(_EnemyState state)
    {
        switch(state)
        {
            case _EnemyState.Trace:
            enemyAnim.SetBool("isTrace",false);
            break;
            case _EnemyState.Attack:
            enemyAnim.ResetTrigger("isAttack");
            break;
            case _EnemyState.Hit:
            enemyAnim.ResetTrigger("isHit");
            break;
            case _EnemyState.Skill:
            enemyAnim.ResetTrigger("isSkill");
            break;
            default:
            break;
        }
    }

    public override IEnumerator OnDamage()
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
            enemyState = _EnemyState.Hit;
        }
    }
}
