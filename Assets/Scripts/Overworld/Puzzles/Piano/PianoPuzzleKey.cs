using System;
using Cysharp.Threading.Tasks;
using FMOD.Studio;
using UnityEngine;

public class PianoPuzzleKey : MonoBehaviour
{
    private PianoPuzzle _puzzle;
    
    [SerializeField]
    private Sprite _normalSprite;

    [SerializeField]
    private Sprite _pressedSprite;

    [SerializeField]
    private FMODUnity.EventReference _playEventRef;

    private FMOD.Studio.EventInstance _playEvent;

    private SpriteRenderer _spriteRenderer;

    private bool _playing;
    private bool _inExample;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _normalSprite;
        _puzzle = GetComponentInParent<PianoPuzzle>();
        _playEvent = FMODUnity.RuntimeManager.CreateInstance(_playEventRef);
    }

    public async UniTask PlayExample()
    {
        await UniTask.SwitchToMainThread();
        _inExample = true;
        _spriteRenderer.sprite = _pressedSprite;
        _playEvent.start();
        await UniTask.WaitForSeconds(1f);
        _playEvent.stop(STOP_MODE.ALLOWFADEOUT);
        _spriteRenderer.sprite = _normalSprite;
        _inExample = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_puzzle.CanPlayKey)
            return;
        if (!other.CompareTag("Player"))
            return;

        _playing = _puzzle.PlayKey(this);

        if (!_playing)
            return;

        _spriteRenderer.sprite = _pressedSprite;
        _playEvent.start();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_playing || _inExample)
            return;
        if (!other.CompareTag("Player"))
            return;

        _spriteRenderer.sprite = _normalSprite;
        _playEvent.getPlaybackState(out var state);

        if (state is not (PLAYBACK_STATE.STOPPED or PLAYBACK_STATE.STOPPING))
            _playEvent.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
