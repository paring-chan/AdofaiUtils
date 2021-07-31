using System.IO;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Editor
{
    internal static class Assets
    {
        public static AssetBundle Bundle { get; private set; }

        public static Sprite Triangle { get; private set; }

        internal static void Init()
        {
            Bundle = AssetBundle.LoadFromFile(
                Path.Combine("Mods", "AdofaiUtils2.Editor", "adofaiutils2.editor.assets"));
            Triangle = Bundle.LoadAsset<Sprite>("Assets/Sprites/Triangle.png");
        }
    }
}