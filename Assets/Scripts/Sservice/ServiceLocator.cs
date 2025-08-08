using System;
using System.Collections.Generic;
using Sservice;
using UnityEngine;

namespace Service
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<System.Type, IService> Services = new();

        public static void Register(System.Type type, IService service)
        {
            if (!Services.TryAdd(type, service))
            {
                Debug.LogWarning($"Service {type.Name} already registered.");
            }
        }

        public static T Get<T>() where T : class, IService
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                return service as T;
            }

            Debug.LogError($"Service {typeof(T).Name} not found.");
            return null;
        }

        public static void Clear()
        {
            Services.Clear();
        }
    }

}
