using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Entity : MonoBehaviour, IDamageable
{
    // 유동 데이터
    public int curHp;                   // 현재 체력
    public float moveSpeed;             // 이동 속도

    // public float attackSpeed;        // 공격 속도
    // public int ignore;               // 방어력 관통
    
    public int defence;                 // 방어력
    public abstract IEnumerator OnHit(int _damage, int _ignore);
}
