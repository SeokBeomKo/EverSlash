using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    public int damage;
    public Transform target;
    public bool isMove; 
    public bool isTrace;
    public bool isAttack;

    public float attackRadius;
    public float attackRange;

    private float ImpactForce = 1000f;
    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    private SkinnedMeshRenderer meshRenderer;
    private Color originColor1;
    private Color originColor2;
    private Color originColor3;
    private EnemyAnimator anim;
    public string debrisName;

    [SerializeField]
    private GameObject attackCollision;
    private void OnEnable()
    {
        curHealth = maxHealth;
        anim.OnTrace(false);
    }
    private void OnDisable() {
        ObjectPooler.ReturnToPool(gameObject);
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<EnemyAnimator>();
        nav = GetComponent<NavMeshAgent>();

        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor1 = meshRenderer.material.GetColor("_BaseColor");
        originColor2 = meshRenderer.material.GetColor("_1st_ShadeColor");
        originColor3 = meshRenderer.material.GetColor("_2nd_ShadeColor");
    }
    private void Update() {
        Move();
    }
    private void FixedUpdate() {
        // 범위 내에 타겟이있는지
        Tracing();

        // 공격 타겟 설정
        Targeting();
    }
    private void Tracing(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f,LayerMask.GetMask("Player"));
        if (hitColliders.Length > 0){
            target = hitColliders[0].transform;
            isTrace = true;
            anim.OnTrace(true);
        }
        else{
            target = null;
            isTrace = false;
            anim.OnTrace(false);
        }
    }
    private void Targeting(){
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 
                                                    attackRadius,
                                                    transform.forward, 
                                                    attackRange,
                                                    LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack){
            StartCoroutine(Attack());
        }
    }

    private void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,10f);
    }

    IEnumerator Attack(){
        isAttack = true;
        anim.OnTrace(false);
        anim.OnAttack();

        yield return new WaitForSeconds(0.1f); // 공격 애니메이션 스피드 변경시 변경해야함
        isAttack = false;
        anim.OnTrace(true);
    }
    private void Move(){
        if (nav.enabled)
        {
            if (target != null)
                nav.SetDestination(target.position);
            nav.isStopped = (!isTrace || isAttack);
        }
    }
    

    public void OnAttackCollision(){
        attackCollision.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "PlayerAttack"){
            int rand = other.GetComponent<AttackCollider>().TurnDamage();
            curHealth -= rand;
            Debug.Log(rand) ;

            anim.TakeDamage();

            Vector3 reactVec = (transform.position - target.transform.position);

            StartCoroutine(OnDamage(reactVec));
        }
    }
    IEnumerator OnDamage(Vector3 reactVec){
        meshRenderer.material.SetColor("_1st_ShadeColor",Color.red);
        meshRenderer.material.SetColor("_2nd_ShadeColor",Color.red);
        meshRenderer.material.SetColor("_BaseColor",Color.red);
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0){
            meshRenderer.material.SetColor("_1st_ShadeColor",originColor1);
            meshRenderer.material.SetColor("_2nd_ShadeColor",originColor2);
            meshRenderer.material.SetColor("_BaseColor",originColor3);
            rigid.AddForce(reactVec.normalized * ImpactForce, ForceMode.Impulse);
        }
        else{
            meshRenderer.material.SetColor("_1st_ShadeColor",originColor1);
            meshRenderer.material.SetColor("_2nd_ShadeColor",originColor2);
            meshRenderer.material.SetColor("_BaseColor",originColor3);
            // 몬스터 죽었을 경우
            // anim.Die();
            // meshRenderer.material.SetColor("_1st_ShadeColor",Color.black);
            // meshRenderer.material.SetColor("_2nd_ShadeColor",Color.black);
            // meshRenderer.material.SetColor("_BaseColor",Color.black);
            // gameObject.layer = 8;
            // nav.enabled = false;
            // isMove = false;
            // Destroy(gameObject, 3);
            isAttack = false;
            GameObject debris = ObjectPooler.SpawnFromPool(debrisName,transform.position);
            debris.GetComponent<EnemyDebris>().Explosion(reactVec);
            
            gameObject.SetActive(false);
        }
    }
}
