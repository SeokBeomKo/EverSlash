using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skill;

public class PlayerSkill : MonoBehaviour
{
    public Player player;
    public List<ActiveSkill> activeSkills;
    public ActiveSkill curSkill;
    private void Awake() 
    {
        activeSkills = new List<ActiveSkill>();
        for (int skillSize = 0; skillSize < 4; skillSize++)
        {
            activeSkills.Add(null);
        }
    }

    public void UseSkill()
    {
        if (Input.GetButtonDown("Skill1"))
        {
            ConditionCheck(0);
        }
        else if (Input.GetButtonDown("Skill2"))
        {
            ConditionCheck(1);
        }
        else if (Input.GetButtonDown("Skill3"))
        {
            ConditionCheck(2);
        }
        else if (Input.GetButtonDown("Skill4"))
        {
            ConditionCheck(3);
        }
    }
    public void ConditionCheck(int number)
    {
        if (null == activeSkills[number])
            {
                Debug.Log("비어있는 슬롯 입니다.");
                return;
            }
            if (SkillCoolTimeCheck(activeSkills[number]))
            {
                curSkill = activeSkills[number];
                player.stateMachine.ChangeState(player.stateMachine.stateDic["SkillState"]);
            }
    }
    public bool SkillCoolTimeCheck(ActiveSkill skill)
    {
        if (skill.coolTime > skill._coolTime)
        {
            Debug.Log("재사용 대기시간" + (skill.coolTime - skill._coolTime));
            return false;
        }
        else
        {
            Debug.Log("스킬 사용");
            return true;
        }
    }

    public void SkillCoolTime()
    {  
        foreach(ActiveSkill value in activeSkills)
        {
            if (null != value)
            value._coolTime += Time.deltaTime;
        }
    }
}
