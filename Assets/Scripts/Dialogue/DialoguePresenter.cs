using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialoguePresenter : MonoBehaviour
{
    [SerializeField]
    private float _charactersPerSecond = 30f;

    [SerializeField]
    private GameObject _dialogueRootUI;
    
    [SerializeField]
    private TextMeshProUGUI _lineTextMesh;

    [SerializeField]
    private Image _characterIcon;

    [SerializeField]
    private TextMeshProUGUI _characterNameTextMesh;

    private TweenerCore<string, string, StringOptions> _tween;

    public bool IsPlaying => _tween is { active: true } && _tween.IsPlaying();

    public void ShowLine(DialogueLine line)
    {
        _dialogueRootUI.SetActive(true);
        _lineTextMesh.text = string.Empty;
        _lineTextMesh.color = line.Character.TextColor;
        _characterIcon.sprite = line.Character.Icon;
        _characterNameTextMesh.SetText(line.Character.Name);

        _tween = GetTextTween(line);
    }

    public void FinishTextAnimation()
    {
        if (IsPlaying)
        {
            _tween.Complete();
        }
    }

    public void EndDialogue()
    {
        _dialogueRootUI.SetActive(false);
    }

    private TweenerCore<string, string, StringOptions> GetTextTween(DialogueLine line)
    {
        return DOTween.To(() => _lineTextMesh.text, text => _lineTextMesh.SetText(text), line.Text, line.GetLineDuration(_charactersPerSecond)).SetEase(Ease.Linear);
    }
}
