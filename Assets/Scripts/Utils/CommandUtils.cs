using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public static class CommandUtils
{
    public static void InitSquadActions<T>(GameObject gameObject, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            IEnumerable<Type> actions = assembly.GetTypes()
                                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (Type type in actions)
            {
                gameObject.AddComponent(type);
            }
        }
    }
}
