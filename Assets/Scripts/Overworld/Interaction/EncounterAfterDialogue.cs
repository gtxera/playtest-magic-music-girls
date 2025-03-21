using UnityEngine;

public class EncounterAfterDialogue : MonoBehaviour, IEventListener<DialogueFinishedEvent>, IEventListener<CombatEndedEvent>
{
    [SerializeField]
    private Dialogue _dialogue;

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

    public void Handle(DialogueFinishedEvent @event)
    {
        if (@event.Dialogue == _dialogue)
            CombatManager.Instance.StartCombat(_encounterData, this);
    }

    public void Handle(CombatEndedEvent @event)
    {
        if (@event.PlayerVictory && @event.EncounterStarter == this)
            Destroy(gameObject);
    }
}
