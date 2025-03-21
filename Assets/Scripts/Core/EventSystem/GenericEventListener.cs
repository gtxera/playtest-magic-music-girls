using UnityEngine;
using UnityEngine.Playables;
using VContainer;

public class GenericEventListener : MonoBehaviour, IEventListener<DialogueFinishedEvent>
{
    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private Dialogue _dialogue;

    [Inject]
    private void Inject(EventBus eventBus)
    {
        eventBus.Subscribe(this);
    }
    
    public void Handle(DialogueFinishedEvent @event)
    {
        if (@event.Dialogue == _dialogue)
        {
            _director.Play();
            Debug.Log("a");
        }
        else
        {
            Debug.Log("b");
        }
    }
}
