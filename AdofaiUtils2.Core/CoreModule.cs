using AdofaiUtils2.Core.Settings;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core
{
    internal static class CoreModule
    {
        private static UnityModManager.ModEntry _modEntry;

        private static SettingsModule _settings;

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            Assets.Init();
            _settings = new SettingsModule();
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Debug.Log(value);
            _modEntry = modEntry;
            return true;
        }
    }
}