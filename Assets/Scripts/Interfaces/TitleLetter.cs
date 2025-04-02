using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TitleLetter : MonoBehaviour
{
    [SerializeField]
    private float _delay;

    [SerializeField]
    private float _animDuration;

    [SerializeField]
    private Vector2 _startOffSet;

    [SerializeField]
    private float _bounceDuration;

    public async UniTask StartAnimation()
    {
        await UniTask.SwitchToMainThread();
        var rect = (RectTransform)transform;

        var startPos = rect.anchoredPosition;

        rect.anchoredPosition += _startOffSet;

        await UniTask.WaitForSeconds(_delay);

        var finished = false;
        rect.DOAnchorPos(startPos, _animDuration).SetEase(Ease.OutBounce).OnComplete(() => finished = true);

        await UniTask.WaitUntil(() => finished);
    }

    public async void StartBounce()
    {
        UniTask.RunOnThreadPool(Bounce);
    }

    private async UniTask Bounce()
    {
        await UniTask.SwitchToMainThread();
        var rect = (RectTransform)transform;
        await UniTask.WaitForSeconds(_delay);

        var sequence = DOTween.Sequence();
        
        var pos = rect.anchoredPosition;
        
        sequence
            .Append(rect.DOAnchorPosY(pos.y +  10f, _bounceDuration/4).SetEase(Ease.OutSine))
            .Append(rect.DOAnchorPosY(pos.y, _bounceDuration/4).SetEase(Ease.InSine))
            .Append(rect.DOAnchorPosY(pos.y - 10f, _bounceDuration/4).SetEase(Ease.OutSine))
            .Append(rect.DOAnchorPosY(pos.y, _bounceDuration/4).SetEase(Ease.InSine))
            .SetLoops(-1, LoopType.Restart);
    }
}
