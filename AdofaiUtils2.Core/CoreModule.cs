using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core
{
    internal static class CoreModule
    {
        private static UnityModManager.ModEntry _modEntry;
        
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
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