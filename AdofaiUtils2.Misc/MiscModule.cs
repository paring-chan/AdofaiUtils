using System.Reflection;
using AdofaiUtils2.Core;
using AdofaiUtils2.Core.Util;
using HarmonyLib;
using UnityModManagerNet;

namespace AdofaiUtils2.Misc
{
    public class MiscModule
    {
        internal static UnityModManager.ModEntry ModEntry;

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
            Harmony.PatchConditionalAll(Assembly.GetExecutingAssembly());
        }

        private static void StopTweaks()
        {
            Harmony.UnpatchConditionalAll(Assembly.GetExecutingAssembly());
        }
    }
}