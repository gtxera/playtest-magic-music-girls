using System;
using System.Collections;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimations : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private SerializedDictionary<string, AnimationClip> _animations;
    
    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private float _damageFlashFrequency = 10f;

    [SerializeField]
    private float _damageFlashDuration = 2f;

    [SerializeField, ColorUsage(false, true)]
    private Color _damageColor = Color.red;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _unit.HealthChanged += OnHealthChanged;
    }

    public void Play(string animationKey)
    {
        if (animationKey == string.Empty)
            return;

        var animation = _animations[animationKey];
        _animator.Play(animation.name);
        CombatAnimationsController.Instance.AddAnimation(animation.length);
        
        if (_unit is PartyUnit partyUnit)
        {
            
        }
    }

    private IEnumerator DamageAnimation()
    {
        CombatAnimationsController.Instance.AddAnimation(_damageFlashDuration);
        var elapsedTime = 0f;
        var totalTime = (_damageFlashDuration * _damageFlashFrequency);

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime * _damageFlashFrequency;
            var floor = Mathf.FloorToInt(elapsedTime);
            _sprite.material.SetColor("_AddedColor", floor % 2 == 0 ? Color.black : _damageColor);
            yield return null;
        }
    }
    
    private void OnHealthChanged(HealthChangedEventArgs args)
    {
        if (args.Change < 0)
            StartCoroutine(DamageAnimation());
    }
}
