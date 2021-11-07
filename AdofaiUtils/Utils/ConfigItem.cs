using System;

namespace AdofaiUtils.Utils
{
    internal class BooleanConfigItem : ConfigItem
    {
        protected bool val = false;

        public override void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
            if (action == SettingsMenu.Interaction.Increment || action == SettingsMenu.Interaction.Decrement)
            {
                if (action == SettingsMenu.Interaction.Increment) setting.PlayArrowAnimation(true);
                if (action == SettingsMenu.Interaction.Decrement) setting.PlayArrowAnimation(false);
                val = !val;
                scrConductor.instance.PlaySfx(2, ignoreListenerPause: true);
                setting.valueLabel.text = val ? "ON" : "OFF";
                OnChange(val, setting, action);
            }
        }

        public override void Init(PauseSettingButton btn)
        {
            btn.valueLabel.text = val ? "ON" : "OFF";
        }

        protected virtual void OnChange(bool value, PauseSettingButton setting, SettingsMenu.Interaction action)
        {
        }
    }

    internal class ConfigItem
    {
        public virtual void Init(PauseSettingButton btn)
        {
        }

        public virtual void OnInteract(PauseSettingButton setting, SettingsMenu.Interaction action)
        {
        }

        protected void RemoveArrowButtons(PauseSettingButton component)
        {
            component.leftArrow.transform.ScaleXY(0.0f, 0.0f);
            component.rightArrow.transform.ScaleXY(0.0f, 0.0f);
        }

        protected void IntCallback(PauseSettingButton setting, SettingsMenu.Interaction action, ref int value)
        {
            if (action == SettingsMenu.Interaction.Increment && value < setting.maxInt)
            {
                setting.PlayArrowAnimation(true);

                scrConductor.instance.PlaySfx(2, ignoreListenerPause: true);

                value++;
                setting.valueLabel.text = value.ToString();
            }

            if (action == SettingsMenu.Interaction.Decrement && value > setting.minInt)
            {
                setting.PlayArrowAnimation(false);

                scrConductor.instance.PlaySfx(2, ignoreListenerPause: true);

                value--;
                setting.valueLabel.text = value.ToString();
            }
        }

        protected void EnumCallback<T>(PauseSettingButton setting, SettingsMenu.Interaction action, ref T value)
            where T : Enum
        {
            var t = typeof(T);
            var names = Enum.GetNames(t);
            var idx = Array.IndexOf(names, Enum.GetName(t, value));
            if (action == SettingsMenu.Interaction.Increment) setting.PlayArrowAnimation(true);
            if (action == SettingsMenu.Interaction.Decrement) setting.PlayArrowAnimation(false);

            scrConductor.instance.PlaySfx(2, ignoreListenerPause: true);

            if (action == SettingsMenu.Interaction.Increment && names.Length - 1 > idx)
            {
                idx++;
            }
            else if (action == SettingsMenu.Interaction.Increment && names.Length - 1 == idx)
            {
                idx = 0;
            }
            else if (action == SettingsMenu.Interaction.Decrement && 0 < idx)
            {
                idx--;
            }
            else if (action == SettingsMenu.Interaction.Decrement && idx == 0)
            {
                idx = names.Length - 1;
            }

            value = (T) Enum.ToObject(t, idx);
            setting.valueLabel.text = value.ToString();
        }
    }
}