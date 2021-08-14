using System.Collections;
using AdofaiUtils2.Settings;
using AdofaiUtils2.Utils;
using MelonLoader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdofaiUtils2.UI
{
    public class UIManager : AdofaiUtils2Base
    {
        public GameObject CanvasRoot;
        public EventSystem EventSystem;
        public Canvas Canvas;
        public static UIManager Instance;

        public static void Init()
        {
            Instance = new UIManager();
        }

        private UIManager()
        {
            MelonLogger.Msg("Initializing UI...");
            CreateRootCanvas();
        }

        
        private void CreateRootCanvas()
        {
            CanvasRoot = new GameObject("AdofaiUtils2Canvas");
            Object.DontDestroyOnLoad(CanvasRoot);
            CanvasRoot.hideFlags |= HideFlags.HideAndDontSave;
            EventSystem = CanvasRoot.AddComponent<EventSystem>();
            Canvas = CanvasRoot.AddComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            Canvas.referencePixelsPerUnit = 100;
            Canvas.sortingOrder = 1000;
            CanvasScaler scaler = CanvasRoot.AddComponent<CanvasScaler>();
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            CanvasRoot.AddComponent<GraphicRaycaster>();
        }
    }
}