using BepInEx;
using UnityEngine;

namespace AdofaiUtils2
{
    [BepInPlugin("dev.pikokr.plugins.adofaiutils2", "AdofaiUtils2", "0.0.1")]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            Debug.Log("sans!");
        }
    }
}