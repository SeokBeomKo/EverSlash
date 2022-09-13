using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (null == activeSkills[0])
            {
                Debug.Log("비어있는 슬롯");
                return;
            }
            if (UseSkillCheck(activeSkills[0]))
            {
                curSkill = activeSkills[0];
                player.stateMachine.ChangeState(player.stateMachine.stateDic["SkillState"]);
            }
        }
        else if (Input.GetButtonDown("Skill2"))
        {
            if (null == activeSkills[1])
            {
                Debug.Log("비어있는 슬롯");
                return;
            }
            if (UseSkillCheck(activeSkills[1]))
            {
                curSkill = activeSkills[1];
                player.stateMachine.ChangeState(player.stateMachine.stateDic["SkillState"]);
            }
        }
        else if (Input.GetButtonDown("Skill3"))
        {
            if (null == activeSkills[2])
            {
                Debug.Log("비어있는 슬롯");
                return;
            }
            if (UseSkillCheck(activeSkills[2]))
            {
                curSkill = activeSkills[2];
                player.stateMachine.ChangeState(player.stateMachine.stateDic["SkillState"]);
            }
        }
        else if (Input.GetButtonDown("Skill4"))
        {
            if (null == activeSkills[3])
            {
                Debug.Log("비어있는 슬롯");
                return;
            }
            if (UseSkillCheck(activeSkills[3]))
            {
                curSkill = activeSkills[3];
                player.stateMachine.ChangeState(player.stateMachine.stateDic["SkillState"]);
            }
        }
    }
    public bool UseSkillCheck(ActiveSkill skill)
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
