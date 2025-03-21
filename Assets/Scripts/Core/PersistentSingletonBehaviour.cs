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