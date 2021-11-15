using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;

namespace AdofaiUtils.Config
{
    [Config]
    internal class CLSGotoEditor : BooleanConfigItem
    {
        public static void Update()
        {
            if (ConfigLoader.Config.clsGotoEditor)
            {
                AdofaiUtils.Instance.HarmonyInstance.TaggedPatch("CLSGotoEditor");
            }
            else
            {
                AdofaiUtils.Instance.HarmonyInstance.TaggedUnPatch("CLSGotoEditor");
            }
        }
        
        public override void Init(PauseSettingButton btn)
        {
            val = ConfigLoader.Config.clsGotoEditor;
            btn.hasDescription = true;
            btn.label.text = "CLS에서 에디터 입장(E)";
            base.Init(btn);
        }

        protected override void OnChange(bool value, PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            ConfigLoader.Config.clsGotoEditor = value;
            Update();
        }
    }
}