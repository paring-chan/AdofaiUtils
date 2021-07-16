using System.IO;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core
{
    public static class Assets
    {
        public static AssetBundle Bundle { get; private set; }
        
        public static Font NotoSansKrRegular { get; private set; }

        internal static void Init()
        {
            Bundle = AssetBundle.LoadFromFile(
                Path.Combine("Mods", "AdofaiUtils2.Core", "adofaiutils2.core.assets"));
            NotoSansKrRegular = Bundle.LoadAsset<Font>("Assets/Fonts/NotoSansKR-Regular.ttf");
        }
    }
}