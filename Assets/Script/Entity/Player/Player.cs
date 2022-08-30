using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Player : Entity
{
    [SerializeField] public Transform           playerModel;         // 플레이어 모델
    [SerializeField] public Animator            playerAnim;          // 플레이어 애니메이터
    [SerializeField] public Rigidbody           playerRigid;         // 플레이어 리지드바디

    public float  hAxis;        // 좌우 이동
    public float  vAxis;        // 상하 이동
    public Vector3 moveVec;     // 이동 방향

    public PlayerStateMachine stateMachine;

    private void Awake() 
    {
        playerModel = transform.GetChild(0);
        playerAnim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
        stateMachine = GetComponent<PlayerStateMachine>();

        Init();
    }

    public void Init()
    {
        StartCoroutine(stateMachine.StartState());
        //curHp = enemyData.enemyInfo.hp;
    }

    private void FixedUpdate() 
    {
        if (null != stateMachine.curPlayerState)
            stateMachine.curPlayerState.Excute();
    }

    abstract public void AttackDelay();         // 공격 준비 행동
    abstract public void Attack();              // 공격 행동
    virtual  public void Death()                // 사망 행동
    {
        // TODO : 플레이어 사망 시
    }                       
    abstract public void Idle();                // 대기 행동
    abstract public void MobileAttack();        // 이동공격 행동
    abstract public void Move();                // 이동 행동
    abstract public void Skill();               // 스킬 행동

    public override IEnumerator OnDamage(int _damage, int _ignore)
    {
        // 피격 데미지 처리
        int damage = _damage - (defence - _ignore);
        curHp -= damage;

        // 사망 처리
        if (0 >= curHp)
        {
            stateMachine.ChangeState(stateMachine.stateDic["DeathState"]);
        }

        material.meshRenderer.material.SetColor("_BaseColor",       Color.white);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  Color.white);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  Color.white);

        yield return new WaitForSeconds(0.1f);

        material.meshRenderer.material.SetColor("_BaseColor",       material.origin_1);
        material.meshRenderer.material.SetColor("_1st_ShadeColor",  material.origin_2);
        material.meshRenderer.material.SetColor("_2nd_ShadeColor",  material.origin_3);
    }
}
