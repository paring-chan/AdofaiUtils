using System;
using AdofaiUtils2.Utils;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
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
            Container = Object.Instantiate(
                Assets.Bundle.LoadAsset<GameObject>("Assets/prefab/SettingsContainer.prefab"));
            Container.transform.SetParent(ui.Canvas.transform, false);
            Container.AddComponent<SettingsContainerBehaviour>();
            Container.SetActive(false);
        }
    }
}