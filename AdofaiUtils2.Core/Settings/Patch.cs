using AdofaiUtils2.Core.Attribute;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    internal static class Patch
    {
        [PatchCondition("AdofaiUtils2.Core.Settings.ValidInputWasTriggeredPatch", "scrController",
            "ValidInputWasTriggered")]
        private static class ScrControllerValidInputWasTriggeredPatch
        {
            public static bool Prefix(ref bool __result)
            {
                if (SettingsUI.Open)
                {
                    __result = false;
                    return false;
                }

                return true;
            }
        }

        [PatchCondition("AdofaiUtils2.Core.Settings.PauseMenuShow", "PauseMenu", "Show")]
        private static class PauseMenuShow
        {
            public static bool Prefix()
            {
                if (SettingsUI.Escape)
                {
                    scrController.instance.TogglePauseGame();
                    SettingsUI.Escape = false;
                    return false;
                }

                return true;
            }
        }
    }
}