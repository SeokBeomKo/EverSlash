using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    abstract public class ActiveSkill : Skill
    {
        public Animation anim;      // 스킬 사용 시 재생 애니메이션

        public float coolTime;      // 재사용 시간
        public float _coolTime;     // 경과 시간
        private void Awake() 
        {

        }
        abstract public void Init();
        abstract public void Use();
    }
}

