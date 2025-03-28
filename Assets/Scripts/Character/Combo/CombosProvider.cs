using System.Collections.Generic;
using UnityEngine;

public class CombosProvider : 
    PersistentSingletonBehaviour<CombosProvider>,
    IEventListener<LevelGainedEvent>
{
    [SerializeField]
    private List<Combo> _initialCombos;

    private HashSet<Combo> _combos;

    public int ComboPoints { get; private set; }

    public IEnumerable<Combo> Combos => _combos;

    private void Awake()
    {
        _combos = new HashSet<Combo>(_initialCombos);
        EventBus.Instance.Subscribe(this);
    }

    public bool IsUnlocked(Combo combo) => _combos.Contains(combo);

    public bool TryUnlock(Combo combo)
    {
        if (ComboPoints <= 0)
            return false;

        ComboPoints--;
        _combos.Add(combo);
        return true;
    }

    public bool MatchesAny(IReadOnlyDictionary<ComboEmotion, int> emotions, out Combo match)
    {
        match = null;

        foreach (var combo in _combos)
        {
            if (combo.Matches(emotions))
            {
                match = combo;
                return true;
            }
        }

        return false;
    }

    public void Handle(LevelGainedEvent @event)
    {
        ComboPoints += @event.LevelsGained;
    }
}
