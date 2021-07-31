using System.Collections.Generic;
using ADOFAI;
using AdofaiUtils2.Core.Attribute;
using UnityEngine;

namespace AdofaiUtils2.Editor.ShapeExpand
{
    internal class ShapeExpandPatches
    {
        [PatchTag("AdofaiUtils2.Editor.ForcePatch")]
        [PatchCondition("AdofaiUtils2.Editor.ShapeExpand.scnEditor.LoadEditorProperties", "scnEditor",
            "LoadEditorProperties")]
        private static class scnEditorLoadEditorProperties
        {
            private static void Postfix(Dictionary<LevelEventCategory, List<LevelEventButton>> ___eventButtons)
            {
                Debug.Log(___eventButtons.Keys);
            }
        }
    }
}