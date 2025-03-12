using System.IO;
using UnityEngine;

public static class Singleton
{
    private static string _singletonsPrefabPath => "Singletons";

    public static T Get<T>() where T : Component
    {
        var instance = GetFromScene<T>();
        if (instance != null)
            return instance;


        instance = GetFromResourcePrefab<T>();
        if (instance != null)
            return instance;

        return GetFromGeneratedGameObject<T>();
    }

    private static T GetFromScene<T>() where T : Component
    {
        return Object.FindFirstObjectByType<T>();
    }

    private static T GetFromResourcePrefab<T>() where T : Component
    {
        var prefab = Resources.Load<T>(GetSingletonPrefabPath<T>());

        return prefab != null ? Object.Instantiate(prefab) : null;
    }

    private static T GetFromGeneratedGameObject<T>() where T : Component
    {
        var generated = new GameObject($"{typeof(T).Name} - Generated");
        return generated.AddComponent<T>();
    }

    private static string GetSingletonPrefabPath<T>()
    {
        return Path.Combine(_singletonsPrefabPath, typeof(T).Name);
    }
}
