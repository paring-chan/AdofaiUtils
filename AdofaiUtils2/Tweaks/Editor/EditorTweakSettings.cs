using System;
using AdofaiUtils2.Utils;

namespace AdofaiUtils2.Tweaks.Editor
{
    [Serializable]
    public class EditorTweakSettings : TweakSettings
    {
        [Utils.attribute.Settings.Label("선택 범위의 비트수 보기")]
        public bool showSelectionBeats;
    }
}