using System;
using System.Collections.Generic;

public static class Resolver
{
    private static readonly Dictionary<Type, object> cache;

    static Resolver()
    {
        cache = new Dictionary<Type, object>();
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
