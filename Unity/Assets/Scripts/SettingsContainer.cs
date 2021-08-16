using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

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
        public GameObject scrollBar;
        
        private void Awake()
        {
            var container = UIFactory.createPanel("Settings", gameObject);
            var scrollView = UIFactory.createPanel("ScrollView", container);
            var scrollRect = scrollView.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
            
            var viewport = UIFactory.createPanel("Viewport", scrollView);
            scrollRect.viewport = viewport.transform as RectTransform;

            var scrollBarObj = Instantiate(scrollBar, scrollView.transform);
            var scrollbar = scrollBarObj.GetComponent<Scrollbar>();

            scrollRect.verticalScrollbar = scrollbar;

            var content = UIFactory.createPanel("Content", viewport);
            
            scrollRect.content = content.transform as RectTransform;
        }
    }
}