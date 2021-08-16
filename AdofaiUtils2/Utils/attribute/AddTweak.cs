using System;
using AdofaiUtils2.Settings;

namespace AdofaiUtils2.Utils.attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AddTweak : Attribute
    {
        public string Name;
        public Type SettingsType;
        public Type PatchesType;

        public AddTweak(string name, Type settingsType, Type patchesType)
        {
            Name = name;
            SettingsType = settingsType;
            PatchesType = patchesType;
        }
    }
}