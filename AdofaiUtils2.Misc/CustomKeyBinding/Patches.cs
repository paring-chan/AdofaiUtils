using System.Collections.Generic;
using AdofaiUtils2.Core.Attribute;

namespace AdofaiUtils2.Misc.CustomKeyBinding
{
    internal static class Patches
    {
        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.scnCLSUpdate", "scnCLS", "Update")]
        private static class ScnCLSUpdate
        {
            public static void Postfix(scnCLS __instance,
                bool ___searchMode, string ___levelToSelect,
                Dictionary<string, bool> ___loadedLevelIsDeleted)
            {
                var settings = MiscModule.Settings;

                if (settings.instantJoinKeyActive && !scrController.instance.paused && !___searchMode &&
                    settings.instantJoinKey.Down())
                {
                    if (___loadedLevelIsDeleted[___levelToSelect]) return;
                    __instance.EnterLevel();
                }
            }
        }
    }
}