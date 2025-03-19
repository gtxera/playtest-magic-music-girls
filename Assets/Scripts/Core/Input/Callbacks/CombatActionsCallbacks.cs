using UnityEngine.InputSystem;

public class CombatActionsCallbacks : InputActions.ICombatActions
{
    private readonly InputCallbacks _onSelect;
    private readonly InputCallbacks _onDeselect;

    private CombatActionsCallbacks(InputCallbacks onSelect, InputCallbacks onDeselect)
    {
        _onSelect = onSelect;
        _onDeselect = onDeselect;
    }
    
    public void OnSelect(InputAction.CallbackContext context)
    {
        _onSelect.Call(context);
    }

    public void OnDeselect(InputAction.CallbackContext context)
    {
        _onDeselect.Call(context);
    }

    public class Builder
    {
        private readonly InputCallbacks _onSelect = new();
        private readonly InputCallbacks _onDeselect = new();
        
        public Builder AddOnSelect(InputCallback callback, InputActionPhase phase)
        {
            _onSelect.Add(callback, phase);
            return this;
        }
        
        public Builder AddOnDeselect(InputCallback callback, InputActionPhase phase)
        {
            _onDeselect.Add(callback, phase);
            return this;
        }

        public InputActions.ICombatActions Build()
        {
            return new CombatActionsCallbacks(_onSelect, _onDeselect);
        }
    }
}