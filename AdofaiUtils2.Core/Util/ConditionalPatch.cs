using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdofaiUtils2.Core.Attribute;
using HarmonyLib;
using Sirenix.Utilities;
using UnityEngine;

namespace AdofaiUtils2.Core.Util
{
    public static class ConditionalPatch
    {
        private static bool Validate(Type type)
        {
            var meta = type.GetCustomAttribute<PatchCondition>();
            if (meta == null) return false;

            var cls = meta.Assembly.GetType(meta.ClassName);

            if ((meta.MinVersion <= GCNS.releaseNumber || meta.MinVersion == -1) &&
                (meta.MaxVersion >= GCNS.releaseNumber || meta.MaxVersion == -1) && cls != null) return true;
            return false;
        }

        public static void PatchConditional(this Harmony harmony, Type patchType)
        {
            var meta = patchType.GetCustomAttribute<PatchCondition>();
            if (meta.IsEnabled)
            {
                Debug.Log($"Patch {meta.Id} is already enabled.");
                return;
            }

            if (!Validate(patchType))
            {
                Debug.Log($"Patch {meta.Id} is not compatible with this version of adofai.");
                return;
            }
            Type declaringType = meta.Assembly.GetType(meta.ClassName);
            if (declaringType == null)
            {
                Debug.Log($"Cannot find type {meta.ClassName}.");
                return;
            }

            try
            {
                harmony.CreateClassProcessor(patchType).Patch();
            }
            catch (Exception)
            {
                Debug.Log($"{meta.Id} patch failed.");
                return;
            }

            meta.IsEnabled = true;
            var meta2 = patchType.GetCustomAttribute<PatchCondition>();
            Debug.Log($"Patch {meta2.Id} was successful.");
        }

        public static void UnpatchConditional(this Harmony harmony, Type type)
        {
            var meta = type.GetCustomAttribute<PatchCondition>();
            if (meta == null) return;
            if (!meta.IsEnabled)
            {
                Debug.Log($"Patch {meta.Id} is not enabled.");
                return;
            }

            var orig = meta.info.method;
            foreach (var patch in type.GetMethods())
            {
                harmony.Unpatch(orig, patch);
            }

            meta.IsEnabled = false;
            Debug.Log($"Unpatched patch {meta.Id}.");
        }

        private static List<Type> GetTypesContainingAttribute<T>(Assembly asm) where T : System.Attribute
        {
            var types = new List<Type>();
            foreach (var type in asm.GetTypes())
            {
                var attr = type.GetCustomAttribute<T>(true);
                if (attr != null)
                {
                    types.Add(type);
                }
            }

            return types;
        }

        public static void PatchConditionalAll(this Harmony harmony, Assembly asm)
        {
            var types = GetTypesContainingAttribute<PatchCondition>(asm);
            foreach (var type in types)
            {
                harmony.PatchConditional(type);
            }
        }

        public static void UnpatchConditionalAll(this Harmony harmony, Assembly asm)
        {
            var types = GetTypesContainingAttribute<PatchCondition>(asm);
            foreach (var type in types)
            {
                harmony.UnpatchConditional(type);
            }
        }

        private static List<Type> FilterTag<T>(Assembly asm, string tag) where T : System.Attribute
        {
            var types = new List<Type>();
            foreach (var type in asm.GetTypes())
            {
                var attr = type.GetCustomAttribute<T>(true);
                var tagAttr = type.GetCustomAttribute<PatchTag>(true);
                if (attr != null && tagAttr != null && tagAttr.Name == tag)
                {
                    types.Add(type);
                }
            }

            return types;
        }
        
        public static void PatchConditionalTag(this Harmony harmony, Assembly asm, string tag)
        {
            var types = GetTypesContainingAttribute<PatchCondition>(asm).FindAll(type =>
            {
                var tagAttr = type.GetCustomAttribute<PatchTag>();
                if (tagAttr == null) return false;
                return tagAttr.Name == tag;
            });
            foreach (var type in types)
            {
                harmony.PatchConditional(type);
            }
        }
        
        public static void UnpatchConditionalTag(this Harmony harmony, Assembly asm, string tag)
        {
            var types = GetTypesContainingAttribute<PatchCondition>(asm).FindAll(type =>
            {
                var tagAttr = type.GetCustomAttribute<PatchTag>();
                if (tagAttr == null) return false;
                return tagAttr.Name == tag;
            });
            foreach (var type in types)
            {
                harmony.UnpatchConditional(type);
            }
        }
    }
}