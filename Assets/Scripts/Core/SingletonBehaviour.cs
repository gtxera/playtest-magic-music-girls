using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
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
        }
        else if (_instance != this)
        {
            Debug.LogErrorFormat("Singleton do tipo {0} duplicado!", typeof(T));
            Destroy(gameObject);
        }
    }
}
