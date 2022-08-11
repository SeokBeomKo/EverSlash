using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebris : MonoBehaviour
{
    [SerializeField] public GameObject enemyObj;
    [SerializeField] public float m_force;
    [SerializeField] public Vector3 reactVec;
    Rigidbody[] rigid;
    Vector3[] pos;

    private void Awake() {
        rigid = gameObject.GetComponentsInChildren<Rigidbody>();
        pos = new Vector3[rigid.Length];
        for (int i = 0; i < rigid.Length; i++)
        {
            pos[i] = rigid[i].transform.localPosition;
            rigid[i].velocity = Vector3.zero;
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < rigid.Length; i++)
        {
            //rigid[i].velocity = Vector3.zero;
            rigid[i].AddExplosionForce(m_force,transform.position - reactVec.normalized, 10f);
        }
        StartCoroutine(Disable());
    }

    private void OnDisable() {
        for (int i = 0; i < rigid.Length; i++)
        {
            rigid[i].transform.localPosition = pos[i];
            rigid[i].velocity = Vector3.zero;
        }
    }

    IEnumerator Disable(){
        yield return new WaitForSeconds(1f);
        enemyObj.SetActive(true);
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

}
