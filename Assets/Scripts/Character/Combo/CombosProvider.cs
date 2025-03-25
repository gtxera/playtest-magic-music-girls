using System.Collections.Generic;
using UnityEngine;

public class CombosProvider : PersistentSingletonBehaviour<CombosProvider>
{
    [SerializeField]
    private List<Combo> _initialCombos;

    private List<Combo> _combos => _initialCombos;

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
}
