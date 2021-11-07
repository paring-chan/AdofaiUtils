using MelonLoader;

namespace AdofaiUtils
{
    public class AdofaiUtils : MelonMod
    {
        public override void OnApplicationStart()
        {
            Assets.Setup();
        }
    }
}