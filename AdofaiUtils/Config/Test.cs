using AdofaiUtils.Attribute;
using AdofaiUtils.Utils;
using MelonLoader;

namespace AdofaiUtils.Config
{
    [Config]
    internal class Test : ConfigItem
    {
        public override void Init(PauseSettingButton btn)
        {
            RemoveArrowButtons(btn);
            btn.type = "Action";
            btn.label.text = "와! 샌즈!";
        }

        public override void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            MelonLogger.Msg("와!");
        }
    }

    [Config]
    internal class Test2 : ConfigItem
    {
        private int value = 1;

        public override void Init(PauseSettingButton btn)
        {
            btn.type = "Int";
            btn.label.text = "와! 샌즈!!!!!";
            btn.minInt = 1;
            btn.maxInt = 10;
            btn.valueLabel.text = value.ToString();
        }

        public override void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            IntCallback(setting, action, ref value);
        }
    }
}