using System;
using System.Collections;
using UnityEngine;

public class CombatMusic : SingletonBehaviour<CombatMusic>, IEventListener<CombatStartedEvent>, IEventListener<CombatEndedEvent>
{
    [SerializeField]
    private FMODUnity.EventReference _musicEventRef;

    private FMOD.Studio.EventInstance _musicEvent;

    protected override void Awake()
    {
        base.Awake();
        EventBus.Instance.Subscribe(this);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(this);
    }

    public void PlayInstrument(string instrument, float duration)
    {
        CombatAnimationsController.Instance.AddAnimation(duration);
        StartCoroutine(PlayInstrumentCoroutine(instrument, duration));
    }

    private IEnumerator PlayInstrumentCoroutine(string instrument, float duration)
    {
        Debug.Log(instrument);
        _musicEvent.setParameterByName(instrument, 1);
        
        yield return new WaitForSeconds(duration);

        _musicEvent.setParameterByName(instrument, 0);
    }

    public void Handle(CombatStartedEvent @event)
    {
        _musicEvent = FMODUnity.RuntimeManager.CreateInstance(_musicEventRef);
        _musicEvent.start();
    }

    public void Handle(CombatEndedEvent @event)
    {
        _musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
