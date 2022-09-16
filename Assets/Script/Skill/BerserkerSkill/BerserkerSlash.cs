using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Berserker", order = 0)]
public class BerserkerSlash : ActiveSkill
{
    public override void Excute()
    {
        player.playerAnim.GetCurrentAnimatorStateInfo(0);
    }

    public override void EnterSkill()
    {
        // 애니메이션 재생
    }
    public override void ExitSkill()
    {
    }
}
