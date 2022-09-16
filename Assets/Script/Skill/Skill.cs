using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    abstract public class Skill : ScriptableObject
    {
        public Player player;
        public string skillName;
        public string skillDescription;
        public Sprite skillIcon;
    }
}

