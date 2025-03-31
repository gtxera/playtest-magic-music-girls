using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PianoPuzzleKey : MonoBehaviour
{
    private PianoPuzzle _puzzle;
    
    [SerializeField]
    private Sprite _normalSprite;

    [SerializeField]
    private Sprite _pressedSprite;

    private SpriteRenderer _spriteRenderer;

    private bool _playing;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _normalSprite;
        _puzzle = GetComponentInParent<PianoPuzzle>();
    }

    public async UniTask PlayExample()
    {
        await UniTask.SwitchToMainThread();
        _spriteRenderer.sprite = _pressedSprite;
        await UniTask.WaitForSeconds(1f);
        _spriteRenderer.sprite = _normalSprite;
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_playing)
            return;
        if (!other.CompareTag("Player"))
            return;

        _spriteRenderer.sprite = _normalSprite;
    }
}
