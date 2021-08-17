using HarmonyLib;
using MelonLoader;

namespace AdofaiUtils2.Tweaks.Editor
{
    internal static class EditorTweakPatches
    {
        [HarmonyPatch(typeof(scnCLS), "Update")]
        private static class CLSUpdate {
            private static void Postfix()
            {
                MelonLogger.Msg("와아");
            }
        }
    }
}