using System;
using AdofaiUtils2.Utils;

namespace AdofaiUtils2.Tweaks.Play
{
    [Serializable]
    public class PlayTweakSettings : TweakSettings
    {
        [Utils.attribute.Settings.Label("배속 플레이시 1.0배 텍스트 숨기기")]
        [Utils.attribute.Settings.PatchTagByConfig("Tweaks.Play.Hide10")]
        public bool hide10;
    }
}