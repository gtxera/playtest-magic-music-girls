using UnityEngine;
using UnityEngine.InputSystem;

public class Input : PersistentSingletonBehaviour<Input>
{
    private InputActions _inputActions;

    [SerializeField]
    private InputContext _inputContext = InputContext.Player;

    protected void Awake()
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
        UpdateCombatMap();
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
    
    public void Add(InputActions.ICombatActions combatActions)
    {
        _inputActions.Combat.AddCallbacks(combatActions);
    }
    
    public void Remove(InputActions.ICombatActions combatActions)
    {
        _inputActions.Combat.RemoveCallbacks(combatActions);
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
            case InputContext.Combat:
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
            case InputContext.Combat:
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
            case InputContext.Combat:
            default:
                _inputActions.Dialogue.Disable();
                break;
        }
    }

    private void UpdateCombatMap()
    {
        switch (_inputContext)
        {
            case InputContext.Combat:
                _inputActions.Combat.Enable();
                break;
            case InputContext.None:
            case InputContext.UI:
            case InputContext.Player:
            case InputContext.Dialogue:
            default:
                _inputActions.Combat.Disable();
                break;
        }
    }
}
