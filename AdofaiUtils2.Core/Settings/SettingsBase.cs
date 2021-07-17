using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public abstract class SettingsBase
    {
        public string TabName;

        public string Id;

        public abstract void GUI();
    }
}