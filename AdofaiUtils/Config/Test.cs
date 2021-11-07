using System;
using System.Linq;
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
            btn.label.text = "와!!!!!!!!!!!!!!!!";
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
            btn.label.text = "와!!!";
            btn.minInt = 1;
            btn.maxInt = 10;
            btn.valueLabel.text = value.ToString();
        }

        public override void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            IntCallback(setting, action, ref value);
        }
    }

    [Config]
    internal class Test3 : ConfigItem
    {
        private TestEnum value = TestEnum.TEST1;

        enum TestEnum
        {
            TEST1,
            TEST2,
            TEST3
        }

        public override void Init(PauseSettingButton btn)
        {
            btn.type = $"Enum:{typeof(TestEnum).FullName}";
            btn.label.text = "와!!!!!!!";
            btn.valueLabel.text = value.ToString();
        }

        public override void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            EnumCallback(setting, action, ref value);
        }
    }
}