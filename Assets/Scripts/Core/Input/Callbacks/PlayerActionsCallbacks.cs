using UnityEngine.InputSystem;

public class PlayerActionsCallbacks : InputActions.IPlayerActions
{
    private readonly InputCallbacks _onMove;
    private readonly InputCallbacks _onInteract;

    private PlayerActionsCallbacks(InputCallbacks onMove, InputCallbacks onInteract)
    {
        _onMove = onMove;
        _onInteract = onInteract;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _onMove.Call(context);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _onInteract.Call(context);
    }
    
    public class Builder
    {
        private InputCallbacks _onMove = new();
        private InputCallbacks _onInteract = new();

        public Builder OnMove(InputCallback callback, InputActionPhase phase)
        {
            _onMove.Add(callback, phase);
            return this;
        }
    
        public Builder OnInteract(InputCallback callback, InputActionPhase phase)
        {
            _onInteract.Add(callback, phase);
            return this;
        }

        public InputActions.IPlayerActions Build()
        {
            return new PlayerActionsCallbacks(_onMove, _onInteract);
        }
    }
}