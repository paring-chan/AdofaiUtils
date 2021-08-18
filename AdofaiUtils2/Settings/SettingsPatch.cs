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
        [HarmonyPatch(typeof(scrController), "ValidInputWasTriggered")]
        private static class ScrControllerValidInputWasTriggered
        {
            private static bool Prefix(ref bool __result)
            {
                if (Utils.Utils.SettingsOpen)
                {
                    __result = false;
                    return false;
                }

                return true;
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
                    scrController.instance.TogglePauseGame();
                    scrController.instance.paused = true;
                    SettingsManager.Instance.UI.Container.SetActive(true);
                    Utils.Utils.SettingsOpen = true;
                }
            }
        }
        // [TaggedPatch("Settings")]
        // [HarmonyPatch(typeof(Input), "GetKeyDown", typeof(KeyCode))]
        // private static class InputGetKeyDown
        // {
        //     private static bool Prefix(KeyCode key, ref bool __result)
        //     {
        //         if (key == KeyCode.Escape && Utils.Utils.SettingsOpen && Input.GetKey(key))
        //         {
        //             __result = false;
        //             
        //             SettingsUI.Instance.Container.SetActive(false);
        //             Utils.Utils.SaveConfig();
        //             Utils.Utils.SettingsOpen = false;
        //             scrController.instance.paused = false;
        //             return false;
        //         }
        //
        //         return true;
        //     }
        // }
    }
}