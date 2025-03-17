using UnityEngine;

public class CombatPosition : MonoBehaviour, IEventListener<CombatEndedEvent>
{
    public bool IsOccupied { get; private set; }

    private Unit _occupant;

    private void Start()
    {
        EventBus.Instance.Subscribe(this);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(this);
    }

    public void Handle(CombatEndedEvent @event)
    {
        ResetPosition();
    }

    public void Occupy(Unit unit)
    {
        IsOccupied = true;
        _occupant = unit;
    }

    private void ResetPosition()
    {
        _occupant = null;
        IsOccupied = false;
    }
}
