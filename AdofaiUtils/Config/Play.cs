using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;

namespace AdofaiUtils.Config
{
    [Config]
    internal class HideSpeedTrialText : BooleanConfigItem
    {
        public override void Init(PauseSettingButton btn)
        {
            val = ConfigLoader.Config.hideSpeedTrialText;
            btn.hasDescription = true;
            btn.label.text = "1.0배속 텍스트 숨기기";
            base.Init(btn);
            Update();
        }

        private void Update()
        {
            if (val)
            {
                AdofaiUtils.Instance.HarmonyInstance.TaggedPatch("HideSpeedTrialText");
            }
            else
            {
                AdofaiUtils.Instance.HarmonyInstance.TaggedUnPatch("HideSpeedTrialText");
            }
        }

        protected override void OnChange(bool value, PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            ConfigLoader.Config.hideSpeedTrialText = value;
            Update();
        }
    }
}