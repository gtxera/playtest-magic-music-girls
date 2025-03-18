using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTree
{
    [field: SerializeField]
    public SkillTreeNode InitialSkill { get; private set; }
}

[Serializable]
public class SkillTreeNode
{
    [field: SerializeField]
    public Skill Skill { get; private set; }

    [SerializeField]
    private SkillTreeNode[] _next;
    
    [field: SerializeField]
    public bool Unlocked { get; set; }

    public IEnumerable<SkillTreeNode> Next => _next;
}