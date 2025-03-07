using UnityEngine;

public class UIActionsBuilder
{
    public InputActions.IUIActions Build()
    {
        return new UIActionsCallbacks();
    }
}
