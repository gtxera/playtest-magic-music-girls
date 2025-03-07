using UnityEngine.InputSystem;

public class PauseActionsCallbacks : InputActions.IPauseActions
{
    private readonly InputCallbacks _onPause;

    private PauseActionsCallbacks(InputCallbacks onPause)
    {
        _onPause = onPause; 
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        _onPause.Call(context);
    }
    
    public class Builder
    {
        private InputCallbacks _onPause = new();

        public Builder OnPause(InputCallback callback, InputActionPhase phase)
        {
            _onPause.Add(callback, phase);
            return this;
        }

        public InputActions.IPauseActions Build()
        {
            return new PauseActionsCallbacks(_onPause);
        }
    }
}