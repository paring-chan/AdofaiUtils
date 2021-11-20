using System.Collections.Generic;
using System.IO;
using ADOFAI;
using AdofaiUtils.Attribute;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace AdofaiUtils.Tweaks
{
    internal static class GotoEditor
    {
        private static KeyCode _key = KeyCode.E;
        
        private static bool _editor;

        [TaggedPatch("CLSGotoEditor")]
        [HarmonyPatch(typeof(scnCLS), "Update")]
        private static class ScnCLSUpdate
        {
            private static void Postfix(scnCLS __instance,
                string ___levelToSelect,
                Dictionary<string, bool> ___loadedLevelIsDeleted)
            {
                if (Input.GetKeyDown(_key))
                {
                    if (___loadedLevelIsDeleted[___levelToSelect]) return;
                    string levelPath = Path.Combine(__instance.loadedLevelDirs[___levelToSelect],
                        "main.adofai");
                    GCS.sceneToLoad = "scnEditor";
                    GCS.customLevelPaths = new string[1];
                    GCS.customLevelPaths[0] = levelPath;
                    GCS.standaloneLevelMode = false;
                    _editor = true;
                    __instance.controller.StartLoadingScene(WipeDirection.StartsFromRight);
                }
            }
        }

        [TaggedPatch("CLSGotoEditor")]
        [HarmonyPatch(typeof(CustomLevel), "LoadAndPlayLevel")]
        private static class CustomLevelLoadAndPlayLevel
        {
            private static void Postfix(CustomLevel __instance)
            {
                if (_editor)
                {
                    _editor = false;
                    __instance.editor.SwitchToEditMode();
                }
            }
        }

        [TaggedPatch("CLSGotoEditor")]
        [HarmonyPatch(typeof(scrController), "CountSpecialInputKeys")]
        private static class ScrControllerCountSpecialInputKeys
        {
            private static void Postfix(ref int __result, scrController __instance)
            {
                if (__instance.CLSMode && Input.GetKeyDown(_key))
                {
                    __result++;
                }
            }
        }
    }
}