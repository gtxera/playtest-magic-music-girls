using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DialogueInteractable : Interactable
{
    [SerializeField]
    private Dialogue _dialogue;
    
    public override void Interact()
    {
        var controller = LifetimeScope.Find<BaseLifetimeScope>().Container.Resolve<DialogueController>();
        controller.StartDialogue(_dialogue);
    }

    public override string InteractionText => "Falar com fulano";
}
