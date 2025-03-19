using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private readonly int _level;
    
    public EnemyCharacter(EnemyCharacterData characterData) : base(characterData)
    {
        _level = characterData.Level;
    }

    public override int Level => _level;
    
    public override IEnumerable<Skill> GetSkills()
    {
        return CharacterData.Skills;
    }
}
