using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerInteractions : MonoBehaviour
{
    private  Input _input;
    private EventBus _eventBus;
    private InputActions.IPlayerActions _playerActions;

    private Interactable _interactable;

    [Inject]
    private void Inject(Input input, EventBus eventBus)
    {
        _input = input;
        _playerActions = new PlayerActionsCallbacks.Builder()
            .OnInteract(OnInteract, InputActionPhase.Performed)
            .Build();
        _input.Add(_playerActions);

        _eventBus = eventBus;
    }

    private void OnInteract(InputAction.CallbackContext _)
    {
        _interactable?.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Interactable>(out _interactable))
            _eventBus.Publish(new InteractionInRangeEvent(_interactable));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<Interactable>(out _interactable)) return;
        _eventBus.Publish(new InteractionOutOfRangeEvent(_interactable));
        _interactable = null;
    }

    private void OnDestroy()
    {
        _input.Remove(_playerActions);
    }
}
