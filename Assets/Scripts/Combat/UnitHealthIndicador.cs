using DG.Tweening;
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

    [SerializeField]
    private float _healthUpdateDuration;

    private void Start()
    {
        _unit.HealthChanged += OnHealthChanged;
        _healthBar.maxValue = _unit.Stats.Health;
        _healthBar.value = _unit.CurrentHealth;
        _healthText.SetText(GetHealthText(_unit.CurrentHealth));
    }

    private void OnHealthChanged(HealthChangedEventArgs args)
    {
        var toHealth = args.To;
        var fromHealth = args.From;
        var animationHealth = fromHealth;

        _healthBar.DOValue(toHealth, _healthUpdateDuration);
        DOTween.To(() => animationHealth, health => animationHealth = health, toHealth, _healthUpdateDuration)
            .SetEase(Ease.OutCirc)
            .OnUpdate(() =>
            {
                _healthText.SetText(GetHealthText(animationHealth));
            });

        CombatAnimationsController.Instance.AddAnimation(new CombatAnimation(_healthUpdateDuration));
    }

    private string GetHealthText(float health) => Mathf.CeilToInt(health).ToString();
}
