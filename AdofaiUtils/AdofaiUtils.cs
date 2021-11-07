using AdofaiUtils.Attribute;
using MelonLoader;

namespace AdofaiUtils
{
    internal class AdofaiUtils : MelonMod
    {
        public override void OnApplicationStart()
        {
            Assets.Setup();
            HarmonyInstance.TaggedPatch("Core");
        }
    }
}