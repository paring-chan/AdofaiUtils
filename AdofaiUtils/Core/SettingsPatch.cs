using System.Collections.Generic;
using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;
using HarmonyLib;
using UnityEngine;

namespace AdofaiUtils.Core
{
    internal static class SettingsPatch
    {
        [TaggedPatch("Core")]
        [HarmonyPatch(typeof(SettingsMenu), "GenerateSettings")]
        private static class SettingsMenuGenerateSettings
        {
            private static void Postfix(SettingsMenu __instance, List<List<PauseSettingButton>>
                ___settingsTabs, List<SettingsTabButton> ___tabButtons)
            {
                var tabButtonObj = Object.Instantiate(__instance.tabButtonPrefab, __instance.tabButtonsContainer);

                var tabButton = tabButtonObj.GetComponent<SettingsTabButton>();

                tabButton.label.text = "AdofaiUtils";
                
                tabButton.SetFocus(false);

                tabButton.name = "AdofaiUtils";
                
                tabButton.label.SetLocalizedFont();

                tabButton.icon.overrideSprite = SpriteUtilities.FromTexture2D(Assets.Bundle.LoadAsset<Texture2D>("Assets/Sprites/settings_icon.png"));

                tabButtonObj.name = "AdofaiUtils";

                ___tabButtons.Add(tabButton);
            }
        }
    }
}