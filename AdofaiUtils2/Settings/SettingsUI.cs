using System;
using AdofaiUtils2.UI;
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
        public GameObject Content;

        public static SettingsUI Instance;

        public static void Init()
        {
            Instance = new SettingsUI();
        }

        public SettingsUI()
        {
            try
            {
                Container = UIFactory.createPanel("Settings", ui.CanvasRoot);
                Container.AddComponent<SettingsContainerBehaviour>();
                var rect = Container.GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(30, 30);
                rect.offsetMax = -new Vector2(30, 30);
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
                throw;
            }
        }
    }
}