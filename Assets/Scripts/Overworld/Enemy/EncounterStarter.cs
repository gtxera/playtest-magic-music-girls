using System;
using UnityEngine;

public class EncounterStarter : MonoBehaviour, IEventListener<CombatEndedEvent>
{
    [SerializeField]
    private EncounterData _encounterData;

    private void Start()
    {
        EventBus.Instance.Subscribe(this);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        CombatManager.Instance.StartCombat(_encounterData, this);
    }

    public void Handle(CombatEndedEvent @event)
    {
        if (@event.PlayerVictory && @event.EncounterStarter == this)
            Destroy(gameObject);
    }
}
