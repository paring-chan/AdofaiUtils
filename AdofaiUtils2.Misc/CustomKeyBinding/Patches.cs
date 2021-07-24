using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ADOFAI;
using AdofaiUtils2.Core.Attribute;
using AdofaiUtils2.Core.Util;
using UnityEngine;

namespace AdofaiUtils2.Misc.CustomKeyBinding
{
    internal static class Patches
    {
        private static bool editor;

        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.CustomLevelLoadAndPlayLevel", "CustomLevel", "LoadAndPlayLevel")]
        private static class ScnEditorStart
        {
            private static MethodBase _printesp = typeof(CustomLevel).GetPrivateMethod("printesp");

            private static readonly MethodBase
                SelectFirstFloor = typeof(scnEditor).GetPrivateMethod("SelectFirstFloor");

            internal static bool Prefix(string levelPath, CustomLevel __instance)
            {
                void Invoke(MethodBase methodBase, params object[] parameters)
                {
                    methodBase.Invoke(__instance, parameters);
                }

                Invoke(_printesp, (object) "");
                int num = __instance.LoadLevel(levelPath) ? 1 : 0;
                if (num == 0)
                    return false;
                __instance.editor.filenameText.text = Path.GetFileName(levelPath);
                __instance.editor.filenameText.fontStyle = FontStyle.Bold;
                __instance.conductor.SetupConductorWithLevelData(__instance.levelData);
                __instance.RemakePath();
                __instance.ReloadAssets();
                DiscordController.instance.UpdatePresence();
                if (editor)
                {
                    editor = false;
                    __instance.editor.Run(SelectFirstFloor);
                    return false;
                }
                else
                {
                    __instance.Play();
                }

                return false;
            }
        }


        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.scrControllerCheckForSpecialInputKeysOrPause", "scrController",
            "CheckForSpecialInputKeysOrPause")]
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
                        check(settings.KeyBinding.CLS.instantJoinKeyActive,
                            settings.KeyBinding.CLS.instantJoinKey.Down()) ||
                        check(settings.KeyBinding.CLS.editorKeyActive, settings.KeyBinding.CLS.editorKey.Down()))
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
        internal static class ScnCLSUpdate
        {
            private static GameObject _infoObject;

            private static LevelInfoBehaviour _levelInfo;

            public static void Postfix(scnCLS __instance,
                bool ___searchMode, string ___levelToSelect,
                Dictionary<string, bool> ___loadedLevelIsDeleted, Dictionary<string, LevelDataCLS> ___loadedLevels)
            {
                if (_infoObject == null)
                {
                    _infoObject = new GameObject();
                }

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

                if (Input.GetKeyDown(KeyCode.Escape) ||
                    MiscModule.Settings.KeyBinding.CLS.infoKeyActive &&
                    MiscModule.Settings.KeyBinding.CLS.infoKey.Down() && !___searchMode)
                {
                    if (_levelInfo != null)
                    {
                        Object.DestroyImmediate(_levelInfo);
                        _levelInfo = null;
                        scrController.instance.paused = false;
                        scrController.instance.audioPaused = false;
                        scrController.instance.enabled = true;
                        Time.timeScale = 1.0f;
                    }
                    else if (!__instance.controller.paused && !Input.GetKeyDown(KeyCode.Escape))
                    {
                        _levelInfo = _infoObject.AddComponent<LevelInfoBehaviour>();
                        _levelInfo.SetMap(___loadedLevels[___levelToSelect], ___levelToSelect);
                        scrController.instance.paused = true;
                        scrController.instance.audioPaused = true;
                        scrController.instance.enabled = false;
                        Time.timeScale = 0.0f;
                    }
                }

                if (MiscModule.Settings.KeyBinding.CLS.editorKeyActive &&
                    MiscModule.Settings.KeyBinding.CLS.editorKey.Down() && !___searchMode &&
                    !scrController.instance.paused)
                {
                    if (___loadedLevelIsDeleted[___levelToSelect]) return;
                    string levelPath = Path.Combine(__instance.loadedLevelDirs[___levelToSelect],
                        "main.adofai");
                    GCS.sceneToLoad = "scnEditor";
                    GCS.customLevelPaths = new string[1];
                    GCS.customLevelPaths[0] = levelPath;
                    GCS.standaloneLevelMode = false;
                    editor = true;
                    __instance.controller.StartLoadingScene(WipeDirection.StartsFromRight);
                    // __instance.editor.SwitchToEditMode();
                    return;
                }
            }
        }

        [PatchTag("AdofaiUtils2.Misc.KeyBinding")]
        [PatchCondition("AdofaiUtils2.Misc.KeyBinding.scnEditorUpdate", "scnEditor", "Update")]
        internal static class ScnEditorUpdate
        {
            private static readonly MethodBase TryQuitToMenu = typeof(scnEditor).GetPrivateMethod("TryQuitToMenu");

            private static bool Prefix(scnEditor __instance)
            {
                var settings = MiscModule.Settings;

                if (settings.KeyBinding.Editor.quitKeyActive && settings.KeyBinding.Editor.quitKey.Down())
                {
                    if (GCS.standaloneLevelMode)
                    {
                        return true;
                    }

                    __instance.Run(TryQuitToMenu);
                }

                return true;
            }
        }
    }
}