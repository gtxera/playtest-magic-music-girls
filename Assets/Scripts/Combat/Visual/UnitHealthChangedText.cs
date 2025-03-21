using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitHealthChangedText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _changeText;

    [SerializeField]
    private Color _positiveColor;

    [SerializeField]
    private Color _negativeColor;

    public void Show(float change, float duration)
    {
        var color = change < 0 ? _negativeColor : _positiveColor;
        _changeText.color = color;
        _changeText.SetText(Formatting.GetFloatText(Mathf.Abs(change)));

        var rect = (RectTransform)transform;
        var direction = Random.Range(-1f, 1f);
        rect.DOAnchorPos(new Vector2(direction, 1f), duration).SetEase(Ease.InSine);
        _changeText.DOColor(new Color(1, 1, 1, 0), duration).SetEase(Ease.InCirc);
    }
}
