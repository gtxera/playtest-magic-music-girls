using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : PersistentSingletonBehaviour<PauseManager>
{
    private Input _input;
    
    private bool _paused;

    private InputContext _previous;

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

    private void Start()
    {
        _input = Input.Instance;
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
        _previous = Input.Instance.CurrentContex;
        Input.Instance.SetInputContext(InputContext.UI);
    }

    public void Unpause()
    {
        Paused = false;
        Input.Instance.SetInputContext(_previous);
    }

    private void PublishPausedEvent()
    {
        EventBus.Instance.Publish(new PausedEvent(_paused));
    }

    private void SetTimeScale()
    {
        Time.timeScale = _paused ? 0 : 1;
    }
}
