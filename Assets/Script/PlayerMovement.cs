using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    UserData playerdata;
    public float playerSpeed;
    private float moveSpeed;
    public int maxHealth;
    public int curHealth;

    public int defence;
    public int minDamage;
    public int maxDamage;
    float  hAxis;
    float  vAxis;
    Vector3 moveVec;
    Vector3 target;

    HealthBar hpSlider;

    private PlayerAnimator anim;

    public List<VFX_PlayerSlash> slashes;
    public GameObject rageObj;
    public GameObject dustObj;
    public GameObject defaultdustObj;
    public GameObject ragedustObj;

    private SkinnedMeshRenderer[] meshRenderer;
    private Color[] originColor1 = new Color[10];
    private Color[] originColor2 = new Color[10];
    private Color[] originColor3 = new Color[10];
    private Color color;

    [SerializeField]
    private GameObject attackCollision;

    private void Start() {
        //playerdata = DataManager.instance.nowPlayer;
    }
    private void Awake() {
        anim = GetComponentInChildren<PlayerAnimator>();
        meshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshRenderer.Length; i++){
            originColor1[i] = meshRenderer[i].material.GetColor("_BaseColor");
            originColor2[i] = meshRenderer[i].material.GetColor("_1st_ShadeColor");
            originColor3[i] = meshRenderer[i].material.GetColor("_2nd_ShadeColor");
        }
        moveSpeed = playerSpeed;
        dustObj = defaultdustObj;
        defence = 1;

        // HUD UI
        // hpSlider = UIManager.instance.healthBar;
    }

    private void Update() {
        Move();
        Dodge();
        Attack();
        Rage();
        Dust();
    }

    public void Save(){
        DataManager.instance.SaveData();
    }

    public void LevelUp(){
        DataManager.instance.nowPlayer.level++;
        Save();
    }
    List<Collider> NoObjectMonster = new List<Collider>();
    List<float> DistanceMonster = new List<float>();
    Collider TargetMonster = null;

    private void AutoTarget(){
        Collider[] Monsters = Physics.OverlapSphere(transform.position, 2f,LayerMask.GetMask("Enemy"));
        if (Monsters.Length != 0){
            for(int i = 0; i < Monsters.Length;i++){
                Vector3 PosStart = transform.position; PosStart.y = 1f;
                Vector3 PosEnd = Monsters[i].transform.position; PosEnd.y = 1f;
                if (Physics.Linecast(PosStart,PosEnd,LayerMask.GetMask("Object"))){
                    Debug.DrawLine(PosStart,PosEnd,Color.red);
                }
                else{
                    Debug.DrawLine(PosStart,PosEnd,Color.green);
                    float Distance = Vector3.Distance(PosStart,PosEnd);
                    NoObjectMonster.Add(Monsters[i]);
                    DistanceMonster.Add(Distance);
                }
            }
            if (NoObjectMonster.Count != 0){
                int num = 0;
                float dis = DistanceMonster[0];
                for (int i = 0; i < DistanceMonster.Count; i++){
                    if (dis > DistanceMonster[i]){
                        dis = DistanceMonster[i];
                        num = i;
                    }
                }
                TargetMonster = NoObjectMonster[num];
            }
            NoObjectMonster.Clear();
            DistanceMonster.Clear();
        }
        else{
            TargetMonster = null;
        }
    }

    private void Attack(){
        if (Input.GetButtonUp("Fire1")){
            anim.OffComboAttack();
            LevelUp();
            Debug.Log(DataManager.instance.nowPlayer.level);
            Debug.Log(playerdata.level);
        }
        if (Input.GetButton("Fire1")){
            // 자동공격 원거리에만 적용
            // AutoTarget();
            // if (TargetMonster != null){
            //     target = (TargetMonster.transform.position - transform.position).normalized;
            //     transform.LookAt(transform.position + target);
            // }
            anim.OnComboAttack();
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,2f);
    }

    private void Dust(){
        if (anim.Get("isMove")){
            dustObj.SetActive(true);
        }
        else{
            dustObj.SetActive(false);
        }
    }
    private void Rage(){
        if (!Input.GetKeyDown("f"))
            return;
        rageObj.SetActive(!rageObj.activeSelf);
        if (rageObj.activeSelf){
            moveSpeed = moveSpeed * 2f;
            anim.OnRage();
            defaultdustObj.SetActive(false);
            dustObj = ragedustObj;
        }   
        else{
            moveSpeed = moveSpeed * 0.5f;
            anim.OffRage();
            ragedustObj.SetActive(false);
            dustObj = defaultdustObj;
        } 
    }
    private void Move(){
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        if (anim.Get("isDodge"))
            return;

        if (TargetMonster == null){
            moveVec = new Vector3(hAxis,0,vAxis).normalized;
            transform.LookAt(transform.position + moveVec);
        }

        if (anim.Get("isAttack"))
            return;
        
        moveVec = new Vector3(hAxis,0,vAxis).normalized;
        transform.LookAt(transform.position + moveVec);
        
        if (0 == hAxis && 0 == vAxis){
            anim.OnIdle();
            return;
        }

        anim.OnMovement(hAxis,vAxis);
        
        transform.position += moveVec * moveSpeed * Time.deltaTime;
    }

    private void Dodge(){
         if (anim.Get("isDodge")){
            transform.position += transform.forward.normalized * moveSpeed * 1.5f * Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump") && !anim.Get("isDodge")){
            anim.OnDodge();
        }
    }

    public void OnAttackCollision(){
        attackCollision.GetComponent<AttackCollider>().SetDamage(minDamage,maxDamage);
        attackCollision.gameObject.SetActive(true);
    }

    private void SlashAttack(int code){
        if (anim.Get("isDodge"))
            return;
        slashes[code].gameObject.SetActive(true);
        StartCoroutine(DisableSlashes(code));
    }

    IEnumerator DisableSlashes(int code){
        yield return new WaitForSeconds(0.5f);
        slashes[code].gameObject.SetActive(false);
    }

    public IEnumerator OnDamage(int damage){
        TakeDamage(damage);
        for (int i = 0; i < meshRenderer.Length; i++){
            meshRenderer[i].material.SetColor("_BaseColor",Color.red);
            meshRenderer[i].material.SetColor("_1st_ShadeColor",Color.red);
            meshRenderer[i].material.SetColor("_2nd_ShadeColor",Color.red);
        }
            yield return new WaitForSeconds(0.1f);
            if (curHealth > 0){
                for (int i = 0; i < meshRenderer.Length; i++){
                    meshRenderer[i].material.SetColor("_BaseColor",originColor1[i]);
                    meshRenderer[i].material.SetColor("_1st_ShadeColor",originColor2[i]);
                    meshRenderer[i].material.SetColor("_2nd_ShadeColor",originColor3[i]);
                }
            }
            else{
                // 죽었을 경우
                // anim.Die();
                for (int i = 0; i < meshRenderer.Length; i++){
                    meshRenderer[i].material.SetColor("_1st_ShadeColor",Color.black);
                    meshRenderer[i].material.SetColor("_2nd_ShadeColor",Color.black);
                    meshRenderer[i].material.SetColor("_BaseColor",Color.black);
                }
            }
    }
    private void TakeDamage(int _damage){
        int damage = _damage - defence;

        if (0 >= damage)
        {
            return;
        }
        else
        {
            curHealth -= damage;
            hpSlider.SetHP(curHealth);
        }
    }
}
