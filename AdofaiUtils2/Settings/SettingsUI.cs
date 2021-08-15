using System;
using AdofaiUtils2.Utils;
using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.Settings
{
    public class SettingsUI : AdofaiUtils2Base
    {
        public GameObject Container;
        public static SettingsUI Instance;

        public static void Init()
        {
            Instance = new SettingsUI();
        }

        public SettingsUI()
        {
            // Container = Object.Instantiate(
            //     Assets.Bundle.LoadAsset<GameObject>("Assets/prefab/SettingsContainer.prefab"), ui.Canvas.transform);
            // Container.AddComponent<SettingsContainerBehaviour>();
            // var content = Container.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
            // try
            // {
            //     Object.Instantiate(SettingsManager.TweakPrefab, content);
            //     Object.Instantiate(SettingsManager.TweakPrefab, content);
            //     Object.Instantiate(SettingsManager.TweakPrefab, content);
            //     Container.SetActive(false);
            // }
            // catch (Exception e)
            // {
            //     MelonLogger.Msg(e);
            //     throw;
            // }
        }
    }
}