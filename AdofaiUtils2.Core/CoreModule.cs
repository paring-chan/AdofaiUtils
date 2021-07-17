using System.Reflection;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core
{
    internal static class CoreModule
    {
        internal static UnityModManager.ModEntry ModEntry;

        private static SettingsModule _settings;

        public static Harmony Harmony
        {
            get;
            private set;
        }

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            Assets.Init();
            _settings = new SettingsModule();
            Harmony = new Harmony(modEntry.Info.Id);
            return true;
        }

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
            _settings.Init();
            Harmony.PatchConditionalAll(Assembly.GetExecutingAssembly());
        }

        private static void StopTweaks()
        {
            _settings.Destroy();
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
        }
    }
}