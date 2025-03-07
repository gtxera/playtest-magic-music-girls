using UnityEngine.InputSystem;

public class DialogueActionsCallbacks : InputActions.IDialogueActions
{
    private readonly InputCallbacks _onSkip;

    private DialogueActionsCallbacks(InputCallbacks onSkip)
    {
        _onSkip = onSkip;
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        _onSkip.Call(context);
    }

    public class Builder
    {
        private readonly InputCallbacks _onSkip = new();
        
        public Builder AddOnSkip(InputCallback callback, InputActionPhase phase)
        {
            _onSkip.Add(callback, phase);
            return this;
        }

        public InputActions.IDialogueActions Build()
        {
            return new DialogueActionsCallbacks(_onSkip);
        }
    }
}