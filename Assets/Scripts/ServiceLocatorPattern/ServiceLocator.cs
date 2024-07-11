using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static readonly Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public static void Unregister<T>()
    {
        services.Remove(typeof(T));
    }

    public static T Get<T>()
    {
        return (T)services[typeof(T)];
    }

}
