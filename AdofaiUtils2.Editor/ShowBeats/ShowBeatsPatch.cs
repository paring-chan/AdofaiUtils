using System;
using System.Globalization;
using AdofaiUtils2.Core.Attribute;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.Editor.ShowBeats
{
    internal class ShowBeatsPatch
    {
        [PatchTag("AdofaiUtils2.Editor.ShowBeats")]
        [PatchCondition("AdofaiUtils2.Editor.ShowBeats.ScnEditorOnSelectedFloorChange", "scnEditor",
            "OnSelectedFloorChange")]
        private static class ScnEditorOnSelectedFloorChange
        {
            private static GameObject _obj;

            private static void Postfix()
            {
                var editor = scnEditor.instance;

                Object.DestroyImmediate(_obj);
                if (editor.selectedFloors.Count < 2) return;

                var first = editor.selectedFloors[0];

                var last = editor.selectedFloors[editor.selectedFloors.Count - 1];

                var firstPos = first.transform.position;
                var lastPos = last.transform.position;

                GameObject gameObject = Object.Instantiate(editor.prefab_editorNum);
                gameObject.transform.position = new Vector3(firstPos.x + ((lastPos.x - firstPos.x) / 2),
                    firstPos.y + ((lastPos.y - firstPos.y) / 2), firstPos.z);
                double beats = 0;

                for (int i = 0; i < editor.selectedFloors.Count - 1; i++)
                {
                    editor.controller.lm.CalculateFloorAngleLengths();
                    var currentFloor = editor.selectedFloors[i];
                    beats += Mathf.Round((float) currentFloor.angleLength * 57.29578f) / 180;
                }

                gameObject.GetComponent<scrLetterPress>().letterText.text =
                    beats.ToString(CultureInfo.InvariantCulture);
                _obj = gameObject;
            }
        }
    }
}