using System;
using System.Collections;
using AdofaiUtils2.Settings;
using AdofaiUtils2.Utils;
using MelonLoader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.UI
{
    public class UIManager : AdofaiUtils2Base
    {
        public GameObject CanvasRoot;

        public EventSystem EventSystem
        {
            get
            {
                if (EventSystem.current != null)
                {
                    return EventSystem.current;
                }
                var sys = CanvasRoot.GetComponent<EventSystem>();
                if (sys == null)
                {
                    sys = CanvasRoot.AddComponent<EventSystem>();
                }

                return sys;
            }
        }
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
            try
            {
                CanvasRoot = new GameObject("AdofaiUtils2Canvas");
                Object.DontDestroyOnLoad(CanvasRoot);
                CanvasRoot.hideFlags |= HideFlags.HideAndDontSave;
                CanvasRoot.layer = 5;
                CanvasRoot.transform.position = new Vector3(0f, 0f, 1f);

                CanvasRoot.DisableComponent<EventSystem>();

                Canvas = CanvasRoot.AddComponent<Canvas>();
                Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                Canvas.referencePixelsPerUnit = 100;
                Canvas.sortingOrder = 998; // under unityexplorer canvas
            
                CanvasScaler scaler = CanvasRoot.AddComponent<CanvasScaler>();
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
                CanvasRoot.AddComponent<GraphicRaycaster>();
            }
            catch (Exception e)
            {
                MelonLogger.Error(e);
                throw;
            }
        }
    }
}