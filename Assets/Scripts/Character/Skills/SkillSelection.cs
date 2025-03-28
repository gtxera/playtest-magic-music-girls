using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillSelection : IEnumerable<Skill>
{
    public const int SKILL_SELECTION_SIZE = 4;
    
    private Skill[] _skills = new Skill[SKILL_SELECTION_SIZE];

    public int Count => _skills.Count(s => s != null);

    public void ChangeSelection(Skill skill, int index)
    {
        if (index >= _skills.Length)
            throw new ArgumentException("Indice maior que quantidade da seleção");

        _skills[index] = skill;
    }

    public void RemoveSelection(int index)
    {
        if (index >= _skills.Length)
            throw new ArgumentException("Indice maior que quantidade da seleção");

        _skills[index] = null;
    }

    public void Add(Skill skill)
    {
        var lastNull = Array.IndexOf(_skills, null);
        _skills[lastNull] = skill;
    }

    public IEnumerator<Skill> GetEnumerator()
    {
        return _skills.Where(s => s != null).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
