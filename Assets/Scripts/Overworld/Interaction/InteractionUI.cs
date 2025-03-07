using TMPro;
using UnityEngine;
using VContainer;

public class InteractionUI : MonoBehaviour, IEventListener<InteractionInRangeEvent>, IEventListener<InteractionOutOfRangeEvent>, IEventListener<InteractedEvent>
{
    [SerializeField]
    private GameObject _interactionRootElement;
    
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private EventBus _eventBus;

    [Inject]
    private void Inject(EventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe(this);
    }
    
    private void Show(Interactable interactable)
    {
        _interactionRootElement.SetActive(true);
        _textMesh.SetText(interactable.InteractionText);
    }

    private void Hide()
    {
        _interactionRootElement.SetActive(false);
    }

    public void Handle(InteractionInRangeEvent @event)
    {
        Show(@event.Interactable);
    }

    public void Handle(InteractionOutOfRangeEvent @event)
    {
        Hide();
    }

    public void Handle(InteractedEvent @event)
    {
        Hide();
    }
}
