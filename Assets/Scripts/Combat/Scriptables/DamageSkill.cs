using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSkill", menuName = "Scriptable Objects/DamageSkill")]
public class DamageSkill : Skill
{
    public override void Execute(Unit unit, IEnumerable<Unit> targets)
    {
        var units = targets as Unit[] ?? targets.ToArray();
        var targetsCount = units.Length;
        if (targetsCount > MaxTargets)
            throw new InvalidOperationException(
                $"A habilidade {BaseName} foi chamada com {targetsCount} alvos e possui um maximo de {MaxTargets}");

        foreach (var target in units)
        {
            target.TakeDamage(BaseValue);
        }
    }
}
