using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnit : Unit
{
    [field: SerializeField]
    public EnemyCharacterData Data { get; private set; }

    public override Sprite Icon => Data.Icon;
    
    private void Awake()
    {
        Character = new EnemyCharacter(Data);
    }
    protected override async UniTask OnUnitTurn()
    {
        await UniTask.WaitForSeconds(1f);
        
        var availableSkills = GetAvailableSkills().ToList();

        var selectionStarted = false;
        Skill skill = null;
        do
        {
            await UniTask.SwitchToMainThread();
            skill = GetSkillToUse(availableSkills);
            selectionStarted = CombatTargetSelector.Instance.TryStartSelection(skill, this);
            availableSkills.Remove(skill);

        } while (!selectionStarted && availableSkills.Count > 0);

        if (selectionStarted)
        {
            await StartAction(skill);
        }
        else
        {
            CombatManager.Instance.SkipTurn();
        }
    }

    private async UniTask StartAction(Skill skill)
    {
        CombatTargetSelector.Instance.AutoSelect(skill.TargetSelectionStrategy);
        await UniTask.WaitForSeconds(2f);
        UniTask.RunOnThreadPool(() => CombatTargetSelector.Instance.ConfirmSelection());
    }

    private Skill GetSkillToUse(List<Skill> availableSkills)
    {
        var useWhenAvailableSkills = availableSkills.Where(s => s.PriorityType == SkillPriorityType.UseWhenAvailable).ToList();

        if (useWhenAvailableSkills.Count > 0)
            return GetSkillFromRandomPriority(useWhenAvailableSkills);

        return GetSkillFromRandomPriority(availableSkills);
    }

    private Skill GetSkillFromRandomPriority(List<Skill> skills)
    {
        var prioritySum = skills.Select(s => s.Priority).Aggregate((total, priority) => total + priority);
        var orderedSkills = skills.OrderBy(s => s.Priority);
        var random = Random.Range(0, prioritySum);
        var priority = 0;

        foreach (var skill in orderedSkills)
        {
            priority += skill.Priority;

            if (priority > random)
                return skill;
        }

        return null;
    }
}
