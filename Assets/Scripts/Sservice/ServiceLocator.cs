using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<Type, MonoBehaviour> services = new();

    public static void Register<T>(T service) where T : MonoBehaviour
    {
        var type = typeof(T);
        if (!services.TryAdd(type, service))
        {
            Debug.LogWarning($"Service {type.Name} already registered.");
        }
    }

    public static T Get<T>() where T : MonoBehaviour
    {
        if (services.TryGetValue(typeof(T), out var service))
        {
            return service as T;
        }

        Debug.LogError($"Service {typeof(T).Name} not found.");
        return null;
    }

    public static void Clear()
    {
        services.Clear();
    }
}