using System;
using System.Collections.Generic;

public static class Resolver
{
    private static readonly Dictionary<Type, object> cache;

    static Resolver()
    {
        cache = new Dictionary<Type, object>();
    }

    public static void Register<T>(object target)
    {
        Register(typeof(T), target);
    }

    public static void Register(Type type, object target)
    {
        cache.Add(type, target);
    }

    public static void Unregister<T>()
    {
        Unregister(typeof(T));
    }

    public static void Unregister(Type type)
    {
        cache.Remove(type);
    }

    public static T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    public static object Resolve(Type type)
    {
        if(cache.TryGetValue(type, out var value))
        {
            return value;
        }

        return default;
    }
}