using System;
using UnityEngine.InputSystem;

public delegate void InputCallback(InputAction.CallbackContext context);

public class InputCallbacks
{
    public InputCallback Started = delegate { };
    public InputCallback Performed = delegate { };
    public InputCallback Canceled = delegate { };

    public void Add(InputCallback callback, InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Started:
                Started = callback;
                break;
            case InputActionPhase.Performed:
                Performed = callback;
                break;
            case InputActionPhase.Canceled:
                Canceled = callback;
                break;
            case InputActionPhase.Disabled:
            case InputActionPhase.Waiting:
            default:
                throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
        }
    }

    public void Call(InputAction.CallbackContext context)
    {
        if (context.started)
            Started(context);
        
        if (context.performed)
            Performed(context);

        if (context.canceled)
            Canceled(context);
    }
}
