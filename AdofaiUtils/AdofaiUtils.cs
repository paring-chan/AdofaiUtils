using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;
using MelonLoader;

namespace AdofaiUtils
{
    internal class AdofaiUtils : MelonMod
    {
        public static AdofaiUtils Instance
        {
            get;
            private set;
        }
            

        public AdofaiUtils()
        {
            Instance = this;
        }
        
        public override void OnApplicationStart()
        {
            ConfigLoader.Load();
            Assets.Setup();
            HarmonyInstance.TaggedPatch("Core");
            TweakPreload.Init();
        }

        public override void OnApplicationQuit()
        {
            ConfigLoader.Save();
        }
    }
}