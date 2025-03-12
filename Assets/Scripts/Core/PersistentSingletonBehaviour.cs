using UnityEngine;

public abstract class PersistentSingletonBehaviour<T> : PersistentSingletonBehaviour where T : PersistentSingletonBehaviour<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Singleton.Get<T>();
            }

            return _instance;
        }
    }
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogErrorFormat("Singleton do tipo {0} duplicado!", typeof(T));
            Destroy(gameObject);
        }
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