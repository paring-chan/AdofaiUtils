using System.Collections.Generic;
using System.Reflection;
using AdofaiUtils2.Utils;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdofaiUtils2.Settings
{
    internal class SettingsPatch
    {
        private static readonly MethodBase UpdateSetting = AccessTools.Method(typeof(SettingsMenu), "UpdateSetting");

        [TaggedPatch("Settings")]
        [HarmonyPatch(typeof(SettingsMenu), "GenerateSettings")]
        private static class SettingsMenuGenerateSettings
        {
            private static void Postfix(SettingsMenu __instance, List<List<PauseSettingButton>> ___settingsTabs)
            {
                GameObject obj = Object.Instantiate(__instance.buttonPrefab, __instance.settingsContainer);
                PauseSettingButton component = obj.GetComponent<PauseSettingButton>();
                component.name = "AdofaiUtils2 Settings Button";
                component.leftArrow.transform.ScaleXY(0.0f, 0.0f);
                component.rightArrow.transform.ScaleXY(0.0f, 0.0f);
                component.SetFocus(false);
                component.label.text = "AdofaiUtils2 설정";

                ___settingsTabs[3].Add(component);
            }
        }

        [TaggedPatch("Settings")]
        [HarmonyPatch(typeof(SettingsMenu), "UpdateSetting")]
        private static class SettingsMenuUpdateSetting
        {
            private static void Postfix(PauseSettingButton setting)
            {
                if (setting.name == "AdofaiUtils2 Settings Button")
                {
                    scrController.instance.pauseMenu.Hide();
                    scrController.instance.paused = true;
                    scrController.instance.audioPaused = true;
                    scrController.instance.enabled = false;
                    Time.timeScale = 0.0f;
                    SettingsManager.Instance.UI.Container.SetActive(true);
                }
            }
        }
    }
}