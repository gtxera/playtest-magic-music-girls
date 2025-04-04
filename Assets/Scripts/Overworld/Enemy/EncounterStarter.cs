using System;
using System.Collections;
using UnityEngine;

public class EncounterStarter : MonoBehaviour, IEventListener<CombatEndedEvent>
{
    [SerializeField]
    private EncounterData _encounterData;

    [SerializeField]
    private SpriteRenderer _areaRenderer;

    [SerializeField]
    private Animator _animator;

    private bool _markedForDestroy;

    private const float DESTROY_ANIMATION_DURATION = 2f;
    
    private void Start()
    {
        EventBus.Instance.Subscribe(this);
    }

    private void OnEnable()
    {
        if (!_markedForDestroy)
            return;

        StartCoroutine(DestroyRoutine());
        _animator.Play("Dead");
    }

    private IEnumerator DestroyRoutine()
    {
        var material = _areaRenderer.material;
        var elapsedTime = 0f;
        var size = material.GetFloat("_Size");
        var step = size / DESTROY_ANIMATION_DURATION;
        while (elapsedTime < DESTROY_ANIMATION_DURATION)
        {
            elapsedTime += Time.deltaTime;
            size -= step * Time.deltaTime;
            material.SetFloat("_Size", size);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_markedForDestroy)
            return;

        if (!other.CompareTag("Player"))
            return;
        
        CombatManager.Instance.StartCombat(_encounterData, this);
    }

    public void Handle(CombatEndedEvent @event)
    {
        if (@event.PlayerVictory && @event.EncounterStarter == this)
            _markedForDestroy = true;
    }
}
