using System;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.Unity
{
    public class UIFactory
    {
            public static GameObject createPanel(string name, [CanBeNull] GameObject parent = null, Color? color = null)
            {
                var obj = new GameObject(name);
                obj.transform.SetParent(parent.transform);
                var rect = obj.AddComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(1, 1);
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                var img = obj.AddComponent<Image>();
                img.color = color ?? Color.white;
                return obj;
            }

            public static GameObject CreateUIObject(string name, GameObject parent, Vector2 size = default)
            {
                var obj = new GameObject(name);

                if (parent)
                    obj.transform.SetParent(parent.transform, false);

                RectTransform rect = obj.AddComponent<RectTransform>();
                rect.sizeDelta = size;
                return obj;
            }

            public static void Init()
        {
            // ScrollBar = Resources.Load<GameObject>("Assets/Prefab/Scrollbar Vertical.prefab");
        }
    }
    
    public class SettingsContainer : MonoBehaviour
    {
        public GameObject toggle;
        [NonSerialized]
        public GameObject Container;
        [NonSerialized]
        public GameObject Content;
        
        private void Awake()
        {
            Container = UIFactory.createPanel("Settings", gameObject);
            // Container.AddComponent<SettingsContainerBehaviour>();
            var rect = Container.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(30, 30);
            rect.offsetMax = -new Vector2(30, 30);
            Content = UIFactory.createPanel("Content", Container);
            var cr = Content.GetComponent<RectTransform>();
            cr.offsetMin = new Vector2(30, 30);
            cr.offsetMax = -new Vector2(30, 30);
            var contentVR = Content.AddComponent<VerticalLayoutGroup>();
            contentVR.childForceExpandHeight = false;
            var tweak = new GameObject("Tweak");
            var tweakVR = tweak.AddComponent<VerticalLayoutGroup>();
            tweakVR.childForceExpandHeight = false;
            var le = tweak.AddComponent<LayoutElement>();
            // le.preferredHeight = 300.0f;
            tweak.transform.SetParent(Content.transform);
            var toggle = Object.Instantiate(this.toggle, tweak.transform);
            // var tt = toggle.transform.GetChild(1).gameObject.GetComponent<Text>();
            // // tt.text = "테스트";
            // // tt.font = RDString.GetFontDataForLanguage(SystemLanguage.Korean).font;
            // // tt.resizeTextForBestFit = true;
        }
    }
}