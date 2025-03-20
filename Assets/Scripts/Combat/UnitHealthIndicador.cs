using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthIndicador : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private TextMeshProUGUI _healthText;

    private void Start()
    {
        _unit.HealthChanged += OnHealthChanged;
        _healthBar.maxValue = _unit.Stats.Health;
        _healthBar.value = _unit.CurrentHealth;
        _healthText.SetText(GetHealthText(_unit.Stats.Health));
    }

    private void OnHealthChanged(HealthChangedEventArgs args)
    {
        _healthBar.value = args.To;
        _healthText.SetText(GetHealthText(args.To));
    }

    private string GetHealthText(float health) => Mathf.CeilToInt(health).ToString();
}
