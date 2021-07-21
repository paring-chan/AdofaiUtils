using System;
using System.Reflection;
using HarmonyLib;

namespace AdofaiUtils2.Core.Util
{
    public static class ReflectionTools
    {
        public static MethodBase GetPrivateMethod(this Type type, string name)
        {
            return type.GetMethod(name, AccessTools.all);
        }

        public static T RunGetValue<T>(this object obj, MethodBase method, params object[] parameters)
        {
            return (T) method.Invoke(obj, parameters);
        }

        public static T RunGetValue<T>(this object obj, MethodBase method)
        {
            return (T) method.Invoke(obj, new object[] { });
        }

        public static void Run(this object obj, MethodBase method, params object[] parameters)
        {
            method.Invoke(obj, parameters);
        }
        
        public static void Run(this object obj, MethodBase method)
        {
            method.Invoke(obj, new object[] { });
        }
    }
}