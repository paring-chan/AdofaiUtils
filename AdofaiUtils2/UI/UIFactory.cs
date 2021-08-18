using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace AdofaiUtils2.UI
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

        public static GameObject Toggle;
        public static GameObject ScrollView;
        public static GameObject Panel;

        public static void Init()
        {
            Toggle = Assets.Bundle.LoadAsset<GameObject>("Assets/Prefab/Toggle.prefab");
            ScrollView = Assets.Bundle.LoadAsset<GameObject>("Assets/Prefab/Scroll View.prefab");
            Panel = Assets.Bundle.LoadAsset<GameObject>("Assets/Prefab/Panel.prefab");
        }

        // public static GameObject CreateScrollBar(GameObject parent, string name, out Scrollbar scrollbar)
        // {
        //     GameObject obj = Object.Instantiate(ScrollBar, parent.transform);
        //     scrollbar = obj.GetComponent<Scrollbar>();
        //     return obj;
        // }
    }
}