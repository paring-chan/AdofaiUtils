using System;
using System.Collections;
using AdofaiUtils2.Settings;
using AdofaiUtils2.UI;
using AdofaiUtils2.Utils;
using MelonLoader;
using UnityEngine;

namespace AdofaiUtils2
{
    public class AdofaiUtils2: MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Initializing...");
            AdofaiUtils2Behaviour.Setup();
            MelonLogger.Msg("Loading Assets...");
            Assets.Init();
            MelonLogger.Msg("Initialized Core.");
            AdofaiUtils2Behaviour.Instance.StartCoroutine(SetupCoro());
            this.HarmonyInstance.TaggedPatch("Settings");
        }

        private static IEnumerator SetupCoro()
        {
            yield return null;
            
            float start = Time.realtimeSinceStartup;
            float delay = 1f;

            while (delay > 0)
            {
                float diff = Math.Max(Time.deltaTime, Time.realtimeSinceStartup - start);
                delay -= diff;
                yield return null;
            }
            
            UIManager.Init();
            SettingsManager.Init();
            MelonLogger.Msg("UI Initialized.");
        }
    }
}