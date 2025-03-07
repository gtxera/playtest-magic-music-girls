using UnityEngine.InputSystem;

public class Input
{
    private readonly InputActions _inputActions;

    private InputContext _inputContext;

    public Input()
    {
        _inputActions = new InputActions();
    }

    public void EnablePause()
    {
        _inputActions.Pause.Enable();
    }

    public void DisablePause()
    {
        _inputActions.Pause.Disable();
    }

    public void SetInputContext(InputContext inputContext)
    {
        _inputContext = inputContext;
        UpdatePlayerMap();
        UpdateUIMap();
        UpdateDialogueMap();
    }

    public void Add(InputActions.IPlayerActions playerActions)
    {
        _inputActions.Player.AddCallbacks(playerActions);
    }

    public void Remove(InputActions.IPlayerActions playerActions)
    {
        _inputActions.Player.RemoveCallbacks(playerActions);
    }
    
    public void Add(InputActions.IUIActions uiActions)
    {
        _inputActions.UI.AddCallbacks(uiActions);
    }

    public void Remove(InputActions.IUIActions uiActions)
    {
        _inputActions.UI.RemoveCallbacks(uiActions);
    }
    
    public void Add(InputActions.IDialogueActions dialogueActions)
    {
        _inputActions.Dialogue.AddCallbacks(dialogueActions);
    }
    
    public void Remove(InputActions.IDialogueActions dialogueActions)
    {
        _inputActions.Dialogue.RemoveCallbacks(dialogueActions);
    }
    
    public void Add(InputActions.IPauseActions pauseActions)
    {
        _inputActions.Pause.AddCallbacks(pauseActions);
    }
    
    public void Remove(InputActions.IPauseActions pauseActions)
    {
        _inputActions.Pause.RemoveCallbacks(pauseActions);
    }
    
    private void UpdatePlayerMap()
    {
        switch (_inputContext)
        {
            case InputContext.Player:
                _inputActions.Player.Enable();
                break;
            case InputContext.None:
            case InputContext.UI:
            case InputContext.Dialogue:
            default:
                _inputActions.Player.Disable();
                break;
        }
    }
    
    private void UpdateUIMap() 
    {
        switch (_inputContext)
        {
            case InputContext.UI:
                _inputActions.UI.Enable();
                break;
            case InputContext.None:
            case InputContext.Player:
            case InputContext.Dialogue:
            default:
                _inputActions.UI.Disable();
                break;
        } 
    }
    
    private void UpdateDialogueMap()
    {
        switch (_inputContext)
        {
            case InputContext.Dialogue:
                _inputActions.Dialogue.Enable();
                break;
            case InputContext.None:
            case InputContext.UI:
            case InputContext.Player:
            default:
                _inputActions.Dialogue.Disable();
                break;
        }
    }
}
