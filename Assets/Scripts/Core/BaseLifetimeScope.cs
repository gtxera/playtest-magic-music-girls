using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BaseLifetimeScope : LifetimeScope
{
    [SerializeField]
    private DialoguePresenter _dialoguePresenter;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EventBus>(Lifetime.Singleton);
        builder.Register<Input>(Lifetime.Singleton);
        builder.Register<PauseManager>(Lifetime.Singleton);
        builder.Register<SceneLoader>(Lifetime.Singleton);
        builder.RegisterComponent(_dialoguePresenter);
        builder.Register<DialogueController>(Lifetime.Transient);
        
        builder.RegisterBuildCallback(LoadAdditiveScenes);
    }

    private void LoadAdditiveScenes(IObjectResolver container)
    {
        
    }
}
