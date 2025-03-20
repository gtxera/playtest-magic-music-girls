using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class CombatAnimationsController : SingletonBehaviour<CombatAnimationsController>
{
    private List<CombatAnimation> _animations = new();

    public async UniTask WaitForAnimations()
    {
        await UniTask.WaitUntil(() => _animations.Count == 0);
    }

    public void AddAnimation(CombatAnimation animation)
    {
        _animations.Add(animation);
        UniTask.RunOnThreadPool(() => animation.WaitForAnimation(() => _animations.Remove(animation)));
    }
}

public class CombatAnimation
{
    public float Duration { get; }

    public CombatAnimation(float duration)
    {
        Duration = duration;
    }

    public async UniTask WaitForAnimation(Action onFinished)
    {
        await UniTask.WaitForSeconds(Duration);
        onFinished();
    }
}