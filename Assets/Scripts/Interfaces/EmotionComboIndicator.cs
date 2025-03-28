using System;
using DG.Tweening;
using UnityEngine;

public class EmotionComboIndicator : MonoBehaviour
{
    [SerializeField]
    private float _popInDuration = 0.66f;

    [SerializeField]
    private float _popOutDuration = 0.33f;
    
    public ComboEmotion Emotion { get; private set; }

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void Initialize(ComboEmotion emotion)
    {
        Emotion = emotion;
        transform.DOScale(Vector3.one, _popInDuration).SetEase(Ease.InOutBounce);
    }

    public void Remove(Action endAnimationCallback)
    {
        var tween = transform.DOScale(Vector3.zero, _popInDuration).SetEase(Ease.InOutBounce);
        tween.onComplete += endAnimationCallback.Invoke;
    }
}
