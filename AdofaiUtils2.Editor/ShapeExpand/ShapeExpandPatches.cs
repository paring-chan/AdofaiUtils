using System.Collections.Generic;
using ADOFAI;
using AdofaiUtils2.Core.Attribute;
using AdofaiUtils2.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdofaiUtils2.Editor.ShapeExpand
{
    internal class ShapeExpandPatches
    {
        [PatchTag("AdofaiUtils2.Editor.ForcePatch")]
        [PatchCondition("AdofaiUtils2.Editor.ShapeExpand.scnEditor.LoadEditorProperties", "scnEditor",
            "LoadEditorProperties")]
        private static class scnEditorLoadEditorProperties
        {
            private static bool __initialized;
            
            private static void Postfix(Dictionary<LevelEventCategory, List<LevelEventButton>> ___eventButtons)
            {
                if (__initialized)
                {
                    return;
                }
                
                int asdf = ___eventButtons[LevelEventCategory.All].Count;

                var editor = scnEditor.instance;
                Debug.Log(___eventButtons[LevelEventCategory.All]);
                GameObject gameObject = Object.Instantiate(editor.prefab_levelEventButton, editor.levelEventPanel);
                RectTransform component1 = gameObject.GetComponent<RectTransform>();
                float x = (float) (5.0 + 75.0 * (asdf % 7));
                gameObject.GetComponent<Button>();
                component1.SetAnchorPosX(x);
                LevelEventButton component2 = gameObject.GetComponent<LevelEventButton>();
                Debug.Log(asdf);
                component2.page = asdf / 7;
                component2.keyCode = asdf % 7 + 1;
                component2.icon.sprite = Assets.Triangle;
                component2.button.onClick.AddListener(() =>
                {
                    Debug.Log("와아아");
                });
                ___eventButtons[LevelEventCategory.All].Add(component2);
                editor.SetCategory(LevelEventCategory.All);
                editor.settingsPanel.Init(GCS.settingsInfo, false);
                editor.levelEventsPanel.Init(GCS.levelEventsInfo, true);

                __initialized = true;
            }
        }
    }
}