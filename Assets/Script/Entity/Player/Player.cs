using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Player : Entity
{
    public PlayerStateMachine stateMachine; 

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
