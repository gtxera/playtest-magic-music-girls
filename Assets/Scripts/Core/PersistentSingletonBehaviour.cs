using UnityEngine;

public abstract class PersistentSingletonBehaviour<T> : PersistentSingletonBehaviour where T : PersistentSingletonBehaviour<T>
{
    public static T Instance { get; private set; }
    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogErrorFormat("Singleton do tipo {0} duplicado!", typeof(T));
            Destroy(this);
            return;
        }

        Instance = (T)this;
    }

    public static void Clear()
    {
        var singleton = FindAnyObjectByType<T>();
        Destroy(singleton.gameObject);
    }
}

public abstract class PersistentSingletonBehaviour : MonoBehaviour
{
    public static void ClearAll()
    {
        var singletons = FindObjectsByType<PersistentSingletonBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var singleton in singletons)
        {
            Destroy(singleton.gameObject);
        }
    }
}