using System.Reflection;
using AdofaiUtils2.Core.Settings;
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

        public static EditorSettings Settings;

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            #if DEBUG
            modEntry.OnUnload = Unload;
            #endif
            Assets.Init();
            Harmony = new Harmony(modEntry.Info.Id);
            return true;
        }
        
        #if DEBUG
        public static bool Unload(UnityModManager.ModEntry modEntry)
        {
            Assets.Bundle.Unload(true);
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
            Settings = SettingsManager.Load<EditorSettings>();
            SettingsManager.Register(Settings);
            if (Settings.ShowBeats)
            {
                EditorModule.Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Editor.ShowBeats");
            }
        }

        private static void StopTweaks()
        {
            SettingsManager.Unregister(Settings);
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
        }
    }
}