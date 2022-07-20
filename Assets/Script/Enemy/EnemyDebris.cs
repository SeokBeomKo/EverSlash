using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebris : MonoBehaviour
{
    [SerializeField] GameObject Prefabs = null;
    [SerializeField] float m_force;
    [SerializeField] Vector3 m_offset = Vector3.zero;
    public Rigidbody[] rigid;
    public Vector3[] pos;

    private void Awake() {
        rigid = gameObject.GetComponentsInChildren<Rigidbody>();
        pos = new Vector3[rigid.Length];
        for (int i = 0; i < rigid.Length; i++){
            pos[i] = rigid[i].transform.localPosition;
            rigid[i].velocity = Vector3.zero;
        }
    }
    private void OnEnable()
    {
    }

    private void OnDisable() {
        for (int i = 0; i < rigid.Length; i++){
            rigid[i].transform.localPosition = pos[i];
            rigid[i].velocity = Vector3.zero;
        }
        ObjectPooler.ReturnToPool(gameObject);
    }
    public void Explosion(Vector3 reactVec){
        for (int i = 0; i < rigid.Length; i++){
            //rigid[i].velocity = Vector3.zero;
            rigid[i].AddExplosionForce(m_force,transform.position - reactVec.normalized, 10f);
        }
        StartCoroutine(Disable(1f));
    }
    IEnumerator Disable(float time){
        yield return new WaitForSeconds(time);
        for (int i = 0; i < rigid.Length; i++){
        }
        gameObject.SetActive(false);
    }

}
