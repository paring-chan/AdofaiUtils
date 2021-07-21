using System.Reflection;
using AdofaiUtils2.Core;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
using HarmonyLib;
using UnityModManagerNet;

namespace AdofaiUtils2.Misc
{
    public class MiscModule
    {
        internal static UnityModManager.ModEntry ModEntry;
        internal static MiscSettings Settings;

        public static Harmony Harmony
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
            Settings = SettingsManager.Load<MiscSettings>();
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
            SettingsManager.Register(Settings);
            if (Settings.keyBindEnabled)
            {
                MiscModule.Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Misc.KeyBinding");
            }
        }

        private static void StopTweaks()
        {
            SettingsManager.Unregister(Settings);
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
            Harmony.UnpatchAll();
        }
    }
}