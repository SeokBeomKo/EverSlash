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

abstract public class Enemy : Entity, IDropExp
{
    [SerializeField] public EnemyData           enemyData;          // 적 타입에 따른 데이터 스크립터블오브젝트
    [SerializeField] public Animator            enemyAnim;          // 적 애니메이터
    [SerializeField] public Transform           target;             // 추적 대상
    [SerializeField] public NavMeshAgent        nav;                // 추적 네비매쉬

    [SerializeField] public GameObject          enemyObj;           // 적 
    [SerializeField] public EnemyDebris         enemyDbris;         // 적 파편

    
    public EnemyMaterial material;     // 메테리얼

    public float attackDelay;

    public EnemyStateMachine stateMachine; 
      

    // 게임 시작시 설정
    private void Awake() 
    {
        target = GameManager.instance.player.transform;

        enemyAnim               = GetComponent<Animator>();
        nav                     = GetComponent<NavMeshAgent>();

        material.meshRenderer   = GetComponentInChildren<SkinnedMeshRenderer>();
        material.origin_1       = material.meshRenderer.material.GetColor("_BaseColor");
        material.origin_2       = material.meshRenderer.material.GetColor("_1st_ShadeColor");
        material.origin_3       = material.meshRenderer.material.GetColor("_2nd_ShadeColor");

        // 적 유동 데이터 적용
        // enemyInfo = enemyData.enemyInfo;
        attackDelay = enemyData.enemyInfo.attackDelay;

        // 적 이동 속도 설정
        nav.speed = enemyData.enemyInfo.moveSpeed;
        nav.acceleration = enemyData.enemyInfo.moveSpeed * 10f;

        // 적 파편 설정
        enemyDbris.enemyObj = enemyObj;
    }

    // 오브젝트 풀링 시작시 설정
    virtual public void OnEnable()
    {
        StartCoroutine(stateMachine.StartState());
        curHp = enemyData.enemyInfo.hp;
    }

    // 죽었을 시 오브젝트 풀링 리턴
    private void OnDisable() 
    {
        ObjectPooler.ReturnToPool(gameObject);
    }

    private void FixedUpdate() 
    {
        if (null != stateMachine.curEnemyState)
            stateMachine.curEnemyState.Excute();
    }

    abstract public void Attack();                      // 공격 행동
    abstract public void Idle();                        // 대기 행동
    abstract public void Trace();                       // 추격 행동
    virtual public void Skill(){}                       // 스킬 행동
    virtual public void Hit()                           // 피격 행동
    {
        // 피격 애니메이션 재생 끝났을 시 대기 상태로 변환
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            stateMachine.ChangeState(stateMachine.stateDic["IdleState"]);
        }
    }
    public virtual void Death()                         // 사망 행동
    {
        enemyObj.SetActive(false);

        enemyDbris.reactVec = transform.position - target.position;
        enemyDbris.gameObject.SetActive(true);
    }                        

    public override IEnumerator OnHit(int _damage, int _ignore)
    {
        // 피격 데미지 처리
        int damage = _damage - (defence - _ignore);
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

        material.meshRenderer.material.SetColor("_BaseColor",       Color.white);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  Color.white);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  Color.white);

        yield return new WaitForSeconds(0.1f);

        material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("PlayerAttack"))
        {
            var temp = other.gameObject.GetComponent<PlayerAttackColl>();
            // 피격 이펙트 처리
            StartCoroutine(OnHit(temp.damage,temp.ignore));
            //StartCoroutine(OnDamage(temp.TurnDamage(),temp.TurnIgnore()));
        }
    }
}
