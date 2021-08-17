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
                Container = Object.Instantiate(UIFactory.ScrollView, ui.CanvasRoot.transform);
                Container.AddComponent<SettingsContainerBehaviour>();
                Container.name = "Settings";
                var tr = Container.transform as RectTransform;
                tr.offsetMin = new Vector2(30, 30);
                tr.offsetMax = -new Vector2(30, 30);
                Content = Container.transform.GetChild(0).GetChild(0).gameObject;
                var contentVR = Content.AddComponent<VerticalLayoutGroup>();
                contentVR.childForceExpandHeight = false;
                contentVR.padding = new RectOffset(30, 30, 30, 30);
                contentVR.spacing = 30.0f;
                Container.SetActive(false);
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
                throw;
            }
        }
    }
}