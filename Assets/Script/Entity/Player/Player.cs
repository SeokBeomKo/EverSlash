using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerMaterial
{
    public Material[] materials;
    public Color[] origin_1;
    public Color[] origin_2;
    public Color[] origin_3;
}
abstract public class Player : Entity
{
    [SerializeField] public Transform           playerModel;         // 플레이어 모델
    [SerializeField] public Animator            playerAnim;          // 플레이어 애니메이터
    [SerializeField] public Rigidbody           playerRigid;         // 플레이어 리지드바디
    [SerializeField] public PlayerMaterial      playerMaterial;      // 플레이어 메테리얼

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

        playerMaterial.materials = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        playerMaterial.origin_1 = new Color[playerMaterial.materials.Length];
        playerMaterial.origin_2 = new Color[playerMaterial.materials.Length];
        playerMaterial.origin_3 = new Color[playerMaterial.materials.Length];
        Debug.Log(playerMaterial.materials.Length);
        for (int i = 0; i < playerMaterial.materials.Length; i++)
        {
            playerMaterial.origin_1[i] = playerMaterial.materials[i].GetColor("_BaseColor");
            playerMaterial.origin_2[i] = playerMaterial.materials[i].GetColor("_1st_ShadeColor");
            playerMaterial.origin_3[i] = playerMaterial.materials[i].GetColor("_2nd_ShadeColor");
        }

        Init();
    }

    public void Init()
    {
        StartCoroutine(stateMachine.StartState());
        //curHp = enemyData.enemyInfo.hp;
    }

    private void Update() 
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
    abstract public void Dodge();               // 회피 행동
    abstract public void Idle();                // 대기 행동
    abstract public void MobileAttack();        // 이동공격 행동
    abstract public void Move();                // 이동 행동
    abstract public void Skill();               // 스킬 행동

    abstract public void OnAttack();

    public override IEnumerator OnHit(int _damage, int _ignore)
    {
        // 피격 데미지 처리
        int damage = _damage - (defence - _ignore);
        curHp -= damage;

        // 사망 처리
        if (0 >= curHp)
        {
            //stateMachine.ChangeState(stateMachine.stateDic["DeathState"]);
        }

        for (int i = 0; i < playerMaterial.materials.Length; i++)
        {
            playerMaterial.materials[i].SetColor("_BaseColor",      Color.red);
            playerMaterial.materials[i].SetColor("_1st_ShadeColor", Color.red);
            playerMaterial.materials[i].SetColor("_2nd_ShadeColor", Color.red);
        }

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < playerMaterial.materials.Length; i++)
        {
            playerMaterial.materials[i].SetColor("_BaseColor",      playerMaterial.origin_1[i]);
            playerMaterial.materials[i].SetColor("_1st_ShadeColor", playerMaterial.origin_2[i]);
            playerMaterial.materials[i].SetColor("_2nd_ShadeColor", playerMaterial.origin_3[i]);
        }
    }
}
