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
        public GameObject scrollView;
        [NonSerialized]
        public GameObject Container;
        [NonSerialized]
        public GameObject Content;
        
        private void Awake()
        {
            Container = Object.Instantiate(scrollView, transform);
            Container.name = "Settings";
            var tr = Container.transform as RectTransform;
            tr.offsetMin = new Vector2(30, 30);
            tr.offsetMax = -new Vector2(30, 30);
            Content = Container.transform.GetChild(0).GetChild(0).gameObject;
            var contentVR = Content.AddComponent<VerticalLayoutGroup>();
            contentVR.childForceExpandHeight = false;
            contentVR.padding = new RectOffset(30, 30, 30, 30);
            contentVR.spacing = 30.0f;
            var tweak = new GameObject("Tweak");
            var tweakVR = tweak.AddComponent<VerticalLayoutGroup>();
            tweakVR.childForceExpandHeight = false;
            var le = tweak.AddComponent<LayoutElement>();
            tweak.transform.SetParent(Content.transform);
            var toggle = Object.Instantiate(this.toggle, tweak.transform);
            toggle.AddComponent<LayoutElement>().preferredHeight = 50.0f;
            var tt = toggle.transform.GetChild(1).gameObject.GetComponent<Text>();
            tt.text = "테스트";
        }
    }
}