using AdofaiUtils.Utils;
using UnityEngine;

namespace AdofaiUtils
{
    public static class Assets
    {
        public static AssetBundle Bundle;
        
        public static void Setup()
        {
            Bundle = AssetBundle.LoadFromMemory(ResourceLoader.ReadFully(System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("AdofaiUtils.Resources.AssetBundles.adofaiutils.assets")));
        }
    }
}