using System.IO;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core
{
    internal static class Assets
    {
        public static AssetBundle Bundle { get; private set; }
        
        public static GUISkin GUISkin { get; private set; }

        internal static void Init()
        {
            Bundle = AssetBundle.LoadFromFile(
                Path.Combine("Mods", "AdofaiUtils2.Core", "adofaiutils2.core.assets"));
            GUISkin = Bundle.LoadAsset<GUISkin>("Assets/GUISkins/DefaultSkin.guiskin");
        }
    }
}