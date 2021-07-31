using System.Reflection;
using AdofaiUtils2.Core.Util;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Editor
{
    #if DEBUG
    [EnableReloading]
    #endif
    public static class EditorModule
    {
        public static UnityModManager.ModEntry ModEntry;
        internal static Harmony Harmony { get; private set; }

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            #if DEBUG
            modEntry.OnUnload = Unload;
            Debug.Log("asdfasfdasdfdasdfadf");
            #endif
            Harmony = new Harmony(modEntry.Info.Id);
            return true;
        }
        
        #if DEBUG
        public static bool Unload(UnityModManager.ModEntry modeNtry)
        {
            StopTweaks();
            return true;
        }
        #endif

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            ModEntry = modEntry;
            if (value)
            {
                StartTweaks();
            }
            else
            {
                StopTweaks();
            }

            return true;
        }

        private static void StartTweaks()
        {
            Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Editor.ForcePatch");
        }

        private static void StopTweaks()
        {
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
        }
    }
}