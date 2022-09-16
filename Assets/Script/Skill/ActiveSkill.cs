using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ActiveSkill : Skill
{
    public Animation anim;

    public float coolTime;      // 재사용 시간
    public float _coolTime;     // 경과 시간
    private void Awake() 
    {

    }
    abstract public void Excute();
    abstract public void EnterSkill();
    abstract public void ExitSkill();
}
