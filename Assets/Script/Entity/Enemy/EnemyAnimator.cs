using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(){
        animator.SetTrigger("OnHit");
    }

    public void OnTrace(bool set){
        animator.SetBool("isWalk",set);             
    }

    public void OnAttack(){
        animator.SetTrigger("isAttack");             
    }

    public void Die(){
        animator.SetTrigger("doDie");
    }
}
