using System.Reflection;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
using HarmonyLib;
using UnityModManagerNet;

namespace AdofaiUtils2.Play
{
    public static class PlayModule
    {
        public static UnityModManager.ModEntry ModEntry;
        public static PlaySettings Settings;

        internal static Harmony Harmony
        {
            get;
            private set;
        }

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
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
            Settings = SettingsManager.Load<PlaySettings>();
            SettingsManager.Register(Settings);
        }

        private static void StopTweaks()
        {
            SettingsManager.Unregister(Settings);
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
            Harmony.UnpatchAll();
        }
    }
}