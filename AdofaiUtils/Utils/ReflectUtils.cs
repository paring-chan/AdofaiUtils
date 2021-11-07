using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace AdofaiUtils.Utils
{
    public class ReflectUtils
    {
        public static IEnumerable<Type> GetTypesWithAttribute<T>(Assembly assembly) where T: System.Attribute {
            foreach(Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(T), true).Length > 0) {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Type> GetTypesWithAttributeAndInherit<T, C>(Assembly assembly) where T: System.Attribute
        {
            foreach (var type in GetTypesWithAttribute<T>(assembly))
            {
                if (typeof(C).IsAssignableFrom(type))
                {
                    yield return type;
                }
            }
        }
    }

    internal static class ReflectionExtensions
    {
        public static MethodBase GetMethodByName(this Type type, string name)
        {
            return type.GetMethod(name, AccessTools.all);
        }
    }
}