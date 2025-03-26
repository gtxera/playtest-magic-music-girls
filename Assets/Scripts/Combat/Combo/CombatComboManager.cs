using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatComboManager
{
    private Dictionary<ComboEmotion, int> _emotions = new();

    private float _energy = 100f;

    public event Action<float> EnergyChanged = delegate { };
    public event Action<IReadOnlyDictionary<ComboEmotion, int>> EmotionsChanged = delegate { };

    public bool IsEnergyFull => _energy >= 100f;

    private Dictionary<ComboEmotion, int> _rollbackPoint;

    public void GenerateEnergy(float energy)
    {
        _energy = Mathf.Min(_energy + energy, 100f);
        EnergyChanged.Invoke(_energy);
    }

    public void GenerateEmotion(ComboEmotion emotion)
    {
        ConsumeEnergy();

        if (!_emotions.TryAdd(emotion, 1))
            _emotions[emotion] += 1;

        EmotionsChanged.Invoke(_emotions);
    }

    public bool HasComboReady(out Combo combo)
    {
        return CombosProvider.Instance.MatchesAny(_emotions, out combo);
    }

    public void UseCombo(Combo combo)
    {
        _rollbackPoint = new Dictionary<ComboEmotion, int>(_emotions);
        _emotions = _emotions.Except(combo.ComboDefinition).ToDictionary(x => x.Key, x => x.Value);
        EmotionsChanged.Invoke(_emotions);
    }

    public void Rollback()
    {
        _emotions = _rollbackPoint;
        EmotionsChanged.Invoke(_emotions);
    }

    private void ConsumeEnergy()
    {
        if (!IsEnergyFull)
            throw new InvalidOperationException("Consumiu energia sem estar completa");

        _energy = 0f;
        EnergyChanged.Invoke(_energy);
    }
}
