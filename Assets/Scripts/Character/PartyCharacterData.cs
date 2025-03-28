using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyCharacterData", menuName = "Scriptable Objects/PartyCharacterData")]
public class PartyCharacterData : CharacterData
{
    public Sprite interfaceBigBackground;
    public Sprite interfaceSmallBackground;
    public Sprite interfaceBigFrame;
    public Sprite interfaceSmallFrame;
    public Sprite interfaceThinFrame;
    public Sprite characterSelectionFrame;

    public string description;
    
    [field: SerializeField]
    public PartyUnit CombatPrefab { get; private set; }
    
    [field: SerializeField]
    public BaseStats StatsGrowth { get; private set; }
    
    [field: SerializeField]
    public EnergyGenerationPercentages EnergyGenerationPercentages { get; private set; }

    [SerializeField]
    private Skill[] _startingSkills;

    public IEnumerable<Skill> StartingSkills => _startingSkills;

}

[Serializable]
public class EnergyGenerationPercentages
{
    [field: SerializeField]
    public float DamagePercentage { get; private set; }
    
    [field: SerializeField]
    public float HealingPercentage { get; private set; }
    
    [field: SerializeField]
    public float ModifierPercentage { get; private set; }
}