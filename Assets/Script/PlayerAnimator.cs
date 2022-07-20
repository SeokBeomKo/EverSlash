using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OnIdle(){
        animator.SetFloat("horizontal", 0);
        animator.SetFloat("vertical", 0);
        animator.SetBool("isMove",false);
    }

    public void OnMovement(float horizontal, float vertical){
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        animator.SetBool("isMove",true);
    }

    public void OnDodge(){
        animator.SetBool("isDodge",true);
        animator.SetBool("isMove",false);
    }

    public void OnComboAttack(){
        animator.SetTrigger("onComboAttack");
        animator.SetBool("isAttack",true);
        animator.SetBool("isMove",false);
    }
    public void OffComboAttack(){
        animator.ResetTrigger("onComboAttack");
    }

    public void OnRage(){
        animator.speed = animator.speed * 2f;
    }
    public void OffRage(){
        animator.speed = animator.speed * 0.5f;
    }

    public void Set(string func, bool set){
        animator.SetBool(func, set);
    }
    public void SetTrig(string func){
        animator.SetTrigger(func);
    }
    public bool Get(string func){
        return animator.GetBool(func);
    }
}
