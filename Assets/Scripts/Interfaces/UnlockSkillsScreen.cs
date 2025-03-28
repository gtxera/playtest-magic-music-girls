using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnlockSkillsScreen : MonoBehaviour
{
    [SerializeField]
    private PartyCharacterData _characterData;

    private PartyCharacter _character;

    [SerializeField]
    private TextMeshProUGUI _skillPoints;

    private void Start()
    {
        _character = Party.Instance.GetFromData(_characterData);
    }

    public void UpdateSkillPoints()
    {
        _skillPoints.SetText(SkillsManager.Instance.GetSkillPoints(_character).ToString());
    }

    public void ShakeText()
    {
        _skillPoints.transform.DOShakePosition(1f);
        var color = _skillPoints.color;
        var tween = _skillPoints.DOColor(Color.red, 0.5f).SetEase(Ease.Linear);
        tween.onComplete += () =>
        {
            _skillPoints.DOColor(color, 0.5f).SetEase(Ease.Linear);
        };
    }
}
