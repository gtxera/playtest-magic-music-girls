using System.Collections.Generic;

public class SkillsManager : PersistentSingletonBehaviour<SkillsManager>, 
    IEventListener<CharacterAddedToPartyEvent>,
    IEventListener<CharacterRemovedFromPartyEvent>,
    IEventListener<LevelGainedEvent>
{
    private Dictionary<PartyCharacter, CharacterSkillState> _skillStates = new();

    public SkillSelection GetSelection(PartyCharacter character) => _skillStates[character].SkillSelection;
    public IEnumerable<Skill> GetAvailable(PartyCharacter character) => _skillStates[character].AvailableSkills;

    private void Awake()
    {
        EventBus.Instance.Subscribe(this);

        foreach (var character in Party.Instance.Characters)
            AddCharacter(character);
    }

    public bool TryUnlock(PartyCharacter character, Skill skill)
    {
        var state = _skillStates[character];
            
            if (state.SkillPoints <= 0)
                return false;
        
        state.AvailableSkills.Add(skill);
        state.SpendPoint();

        var selection = GetSelection(character);
        if (selection.Count < SkillSelection.SKILL_SELECTION_SIZE)
            selection.Add(skill);

        return true;
    }

    public void Handle(CharacterAddedToPartyEvent @event)
    {
        AddCharacter(@event.AddedCharacter);
    }

    public void Handle(CharacterRemovedFromPartyEvent @event)
    {
        _skillStates.Remove(@event.RemovedCharacter);
    }
    
    public void Handle(LevelGainedEvent @event)
    {
        foreach (var state in _skillStates.Values)
            state.GainPoints(@event.LevelsGained);
    }

    private void AddCharacter(PartyCharacter character)
    {
        var state = new CharacterSkillState();

        foreach (var skill in character.PartyCharacterData.StartingSkills)
        {
            state.SkillSelection.Add(skill);
            state.AvailableSkills.Add(skill);
        }
        
        _skillStates.Add(character, state);
    }

    private class CharacterSkillState
    {
        public SkillSelection SkillSelection { get; private set; } = new();
        public HashSet<Skill> AvailableSkills { get; private set; } = new();
        public int SkillPoints { get; private set; } = Party.Instance.Level - 1;

        public void GainPoints(int points) => SkillPoints += points;
        public void SpendPoint() => SkillPoints--;
    }
}