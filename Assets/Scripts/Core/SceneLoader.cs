using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneLoader
{
    private readonly LifetimeScope _scope;
    
    public SceneLoader(LifetimeScope scope)
    {
        _scope = scope;
    }
    
    public void LoadAdditiveScene(AdditiveScene scene)
    {
        var index = (int)scene;

        using (LifetimeScope.EnqueueParent(_scope))
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        }
    }

    public void UnloadAdditiveScene(AdditiveScene scene)
    {
        var index = (int)scene;

        SceneManager.UnloadSceneAsync(index);
    }
}
