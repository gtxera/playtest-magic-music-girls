using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager
{
    private readonly EventBus _eventBus;
    private readonly Input _input;
    
    private bool _paused;

    private bool Paused
    {
        get => _paused;
        
        set
        {
            _paused = value;
            PublishPausedEvent();
            SetTimeScale();
        }
    }

    public PauseManager(EventBus eventBus, Input input)
    {
        _eventBus = eventBus;
        _input = input;
        _input.EnablePause();
        _input.Add(new PauseActionsCallbacks.Builder().OnPause(TogglePause, InputActionPhase.Performed).Build());
    }
    
    private void TogglePause(InputAction.CallbackContext _)
    {
        Paused = !Paused;
        Debug.Log("pause");
    }

    public void Pause()
    {
        Paused = true;
    }

    public void Unpause()
    {
        Paused = false;
    }

    private void PublishPausedEvent()
    {
        _eventBus.Publish(new PausedEvent(_paused));
    }

    private void SetTimeScale()
    {
        Time.timeScale = _paused ? 0 : 1;
    }
}
