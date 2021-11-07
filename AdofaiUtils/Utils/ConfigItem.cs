namespace AdofaiUtils.Utils
{
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
                value++;
                setting.valueLabel.text = value.ToString();
            }

            if (action == SettingsMenu.Interaction.Decrement && value > setting.minInt)
            {
                value--;
                setting.valueLabel.text = value.ToString();
            }
        }
    }
}