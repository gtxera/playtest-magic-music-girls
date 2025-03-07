using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DialogueLifetimeScope : LifetimeScope
{
    [SerializeField]
    private DialoguePresenter _dialoguePresenter;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<DialogueController>(Lifetime.Transient);
        builder.RegisterComponent(_dialoguePresenter);
    }
}
