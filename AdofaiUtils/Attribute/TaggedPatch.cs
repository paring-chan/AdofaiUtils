using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MelonLoader;

namespace AdofaiUtils.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class TaggedPatch : System.Attribute
    {
        public string Name { get; }

        public TaggedPatch(string tag)
        {
            Name = tag;
        }
    }

    internal static class TaggedPatchExtend
    {
        private static List<Type> FilterTag<T>(Assembly asm, string tag) where T : System.Attribute
        {
            var types = new List<Type>();
            foreach (var type in asm.GetTypes())
            {
                var attr = type.GetCustomAttribute<T>(true);
                var tagAttr = type.GetCustomAttribute<TaggedPatch>(true);
                if (attr != null && tagAttr != null && tagAttr.Name == tag)
                {
                    types.Add(type);
                }
            }

            return types;
        }
        
        public static void TaggedPatch(this HarmonyLib.Harmony harmony, string tag)
        {
            var types = FilterTag<TaggedPatch>(Assembly.GetExecutingAssembly(), tag);
            foreach (var type in types)
            {
                harmony.CreateClassProcessor(type).Patch();
            }
            MelonLogger.Msg($"Patched tag {tag}");
        }
        
        public static void TaggedUnPatch(this HarmonyLib.Harmony harmony, string tag)
        {
            var types = FilterTag<TaggedPatch>(Assembly.GetExecutingAssembly(), tag);
            foreach (var type in types)
            {
                var meta = type.GetCustomAttribute<HarmonyPatch>();
                var orig = meta.info.method;
                foreach (var methodInfo in type.GetMethods())
                {
                    harmony.Unpatch(orig, methodInfo);
                }
            }
            MelonLogger.Msg($"Unpatched tag {tag}");
        }
    }
}