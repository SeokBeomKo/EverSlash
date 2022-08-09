using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntityMaterial
{
    public SkinnedMeshRenderer meshRenderer;
    public Color origin_1;
    public Color origin_2;
    public Color origin_3;
}
abstract public class Entity : MonoBehaviour, IDamageable
{
    public int maxHp;                   // 최대 체력
    public int curHp;                   // 현재 체력
    public float moveSpeed;             // 이동 속도
    public float attackSpeed;           // 공격 속도
    public int attackMin;               // 최소 공격력
    public int attackMax;               // 최대 공격력
    public int ignore;                  // 방어력 관통
    public int defence;                 // 방어력
    public EntityMaterial material;     // 메테리얼
    public abstract IEnumerator OnDamage(int damage);
}
