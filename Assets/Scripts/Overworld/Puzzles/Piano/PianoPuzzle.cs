using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PianoPuzzle : Interactable
{
    [FormerlySerializedAs("_keys")]
    [SerializeField]
    private PianoPuzzleKey[] _orderKeys;

    [SerializeField]
    private PianoPuzzleKey[] _allKeys;

    [SerializeField]
    private UnityEvent _onPuzzleComplete;

    [SerializeField]
    private bool _inProgress;
    [SerializeField]
    private bool _playingMelody;
    private bool _finished;

    public bool CanPlayKey => _inProgress && !_playingMelody;

    public override bool CanInteract => !_inProgress && !_finished;

    private int _currentKeyIndex;

    public override void Interact()
    {
        _inProgress = true;
        UniTask.RunOnThreadPool(StartPuzzle);
    }

    public override string InteractionText => "Repita a melodia na sequencia";

    public bool PlayKey(PianoPuzzleKey key)
    {
        if (key == _orderKeys[_currentKeyIndex])
        {
            _currentKeyIndex++;
            
            if (_currentKeyIndex != _orderKeys.Length)
                return true;
            
            _finished = true;
            _onPuzzleComplete.Invoke();
            _inProgress = false;
            return true;
        }

        UniTask.RunOnThreadPool(FailPuzzle);
        return false;
    }

    private async UniTask StartPuzzle()
    {
        _currentKeyIndex = 0;
        _playingMelody = true;
        
        foreach (var key in _orderKeys)
            await key.PlayExample();

        _playingMelody = false;
    }

    private async UniTask FailPuzzle()
    {
        _playingMelody = true;
        await UniTask.WhenAll(_allKeys.Select(key => key.PlayExample()));
        _playingMelody = false;
        _inProgress = false;
    }
}
