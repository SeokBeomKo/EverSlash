using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Player player;

    private void Awake() 
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion
    }

    public void Init()
    {
        player.playerSkill.activeSkills = null;
        player.playerSkill.curSkill = null;
    }
}
