using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : PersistentSingletonBehaviour<Party>, IEventListener<CombatEndedEvent>
{
    [SerializeField]
    private PartyCharacterData[] _initialCharacters;
    
    private HashSet<PartyCharacter> _characters = new HashSet<PartyCharacter>();

    public float Experience { get; private set; }

    public int Level
    {
        get
        {
            var experience = 0f;
            var level = 0;
            do
            {
                level++;
                experience = LevelCalculator.GetXpRequirementFor(level);
            }
            while (experience < Experience);

            return level;
        }
    }

    public IReadOnlyCollection<PartyCharacter> Characters => _characters;

    public PartyCharacter GetFromData(PartyCharacterData data)
    {
        return _characters.First(c => c.CharacterData == data);
    }

    public void AddCharacter(PartyCharacterData characterData)
    {
        var character = new PartyCharacter(characterData, this);
        _characters.Add(character);
    }

    public void RemoveCharacter(PartyCharacterData characterData)
    {
        _characters.RemoveWhere(c => c.CharacterData == characterData);
    }

    public void AddXp(float xp)
    {
        var experienceBefore = Experience;
        var levelBefore = Level;
        Experience += xp;

        var eventBus = EventBus.Instance;

        eventBus.Publish(new ExperienceGainedEvent(experienceBefore, Experience));
        if (levelBefore != Level)
        {
            eventBus.Publish(new LevelGainedEvent(levelBefore, Level));
        }
    }

    public void Handle(CombatEndedEvent @event)
    {
        if (!@event.PlayerVictory)
            return;

        AddXp(@event.ExperienceReward);
    }

    private void Start()
    {
        foreach (var data in _initialCharacters)
        {
            AddCharacter(data);
        }
    }
}
