using System.Collections.Generic;
using AdofaiUtils2.Core.Attribute;
using UnityModManagerNet;

namespace AdofaiUtils2.Misc.CustomKeyBinding
{
    internal static class Patches
    {
        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.scrControllerCheckForSpecialInputKeysOrPause", "scrController", "CheckForSpecialInputKeysOrPause")]
        internal static class ScrControllerCheckForSpecialInputKeysOrPause
        {
            internal static bool Prefix(scrController __instance, ref bool __result)
            {
                bool check(bool enabled, bool down)
                {
                    return enabled && down;
                }

                var settings = MiscModule.Settings;

                if (__instance.CLSMode)
                {
                    if (check(settings.KeyBinding.CLS.reloadKeyActive, settings.KeyBinding.CLS.reloadKey.Down()) ||
                        check(settings.KeyBinding.CLS.workshopKeyActive, settings.KeyBinding.CLS.workshopKey.Down()) ||
                        check(settings.KeyBinding.CLS.instantJoinKeyActive, settings.KeyBinding.CLS.instantJoinKey.Down()))
                    {
                        __result = true;
                        return false;
                    }
                }

                return true;
            }
        }

        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.scnCLSUpdate", "scnCLS", "Update")]
        private static class ScnCLSUpdate
        {
            public static void Postfix(scnCLS __instance,
                bool ___searchMode, string ___levelToSelect,
                Dictionary<string, bool> ___loadedLevelIsDeleted)
            {
                var settings = MiscModule.Settings;

                if (settings.KeyBinding.CLS.instantJoinKeyActive && !scrController.instance.paused && !___searchMode &&
                    settings.KeyBinding.CLS.instantJoinKey.Down())
                {
                    if (___loadedLevelIsDeleted[___levelToSelect]) return;
                    __instance.EnterLevel();
                }

                if (settings.KeyBinding.CLS.reloadKeyActive && !scrController.instance.paused && !___searchMode &&
                    settings.KeyBinding.CLS.reloadKey.Down())
                {
                    __instance.Refresh();
                }

                if (settings.KeyBinding.CLS.workshopKeyActive && !scrController.instance.paused && !___searchMode &&
                    settings.KeyBinding.CLS.workshopKey.Down())
                {
                    SteamWorkshop.OpenWorkshop();
                }
            }
        }
    }
}