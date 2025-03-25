using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatComboManager
{
    private Dictionary<ComboEmotion, int> _emotions;

    private float _energy;

    public event Action<float> EnergyChanged = delegate { };

    public bool IsEnergyFull => _energy >= 100f;

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
    }

    public bool HasComboReady(out Combo combo)
    {
        return CombosProvider.Instance.MatchesAny(_emotions, out combo);
    }

    private void ConsumeEnergy()
    {
        if (!IsEnergyFull)
            throw new InvalidOperationException("Consumiu energia sem estar completa");

        _energy = 0f;
        EnergyChanged.Invoke(_energy);
    }
}
