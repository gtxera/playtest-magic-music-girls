using System;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
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
}
