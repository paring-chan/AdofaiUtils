using System;
using System.Collections.Generic;
using System.Reflection;
using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdofaiUtils.Core
{
    internal static class SettingsPatch
    {
        private static Dictionary<PauseSettingButton, ConfigItem> _configItems =
            new Dictionary<PauseSettingButton, ConfigItem>();

        private static MethodBase UpdateSetting = typeof(SettingsMenu).GetMethodByName("UpdateSetting");

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

                tabButton.icon.overrideSprite =
                    SpriteUtilities.FromTexture2D(
                        Assets.Bundle.LoadAsset<Texture2D>("Assets/Sprites/settings_icon.png"));

                tabButtonObj.name = "AdofaiUtils";

                ___tabButtons.Add(tabButton);

                List<PauseSettingButton> buttons = new List<PauseSettingButton>();

                foreach (var item in ReflectUtils.GetTypesWithAttributeAndInherit<ConfigAttribute, ConfigItem>(
                    Assembly.GetExecutingAssembly()))
                {
                    ConfigItem instance = Activator.CreateInstance(item) as ConfigItem;
                    if (instance == null) continue;
                    PauseSettingButton component = Object
                        .Instantiate(__instance.buttonPrefab, __instance.settingsContainer)
                        .GetComponent<PauseSettingButton>();
                    component.SetFocus(false);
                    instance.Init(component);
                    component.valueLabel.SetLocalizedFont();
                    component.label.SetLocalizedFont();
                    UpdateSetting.Invoke(__instance, new object[]
                    {
                        component,
                        SettingsMenu.Interaction.Refresh
                    });
                    _configItems[component] = instance;
                    buttons.Add(component);
                }

                ___settingsTabs.Add(buttons);
            }
        }

        [TaggedPatch("Core")]
        [HarmonyPatch(typeof(SettingsMenu), "UpdateSetting")]
        private static class SettingsMenuUpdateSetting
        {
            private static void Postfix(PauseSettingButton setting, SettingsMenu.Interaction action)
            {
                if (action == SettingsMenu.Interaction.Refresh)
                {
                    return;
                }

                var btn = _configItems[setting];
                if (btn != null)
                {
                    btn.OnInteract(setting, action);
                }
            }
        }
    }
}