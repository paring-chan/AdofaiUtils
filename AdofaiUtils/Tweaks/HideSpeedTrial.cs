using AdofaiUtils.Attribute;
using HarmonyLib;
using UnityEngine.UI;

namespace AdofaiUtils.Tweaks
{
    internal static class HideSpeedTrial
    {
        [TaggedPatch("HideSpeedTrialText")]
        [HarmonyPatch(typeof(scrController), "Update")]
        private class ScrControllerUpdate
        {
            private static void Prefix(scrController __instance)
            {
                if (__instance.txtCaption != null &&
                    GCS.speedTrialMode && GCS.currentSpeedTrial.ToString("0.0") == "1.0")
                {
                    __instance.txtCaption.text = __instance.caption;
                }
            }
        }
        
        [TaggedPatch("HideSpeedTrialText")]
        [HarmonyPatch(typeof(scrCountdown), "Update")]
        private static class ScrCountdownUpdate
        {
            private static void Postfix(Text ___text)
            {
                if (GCS.speedTrialMode && GCS.currentSpeedTrial.ToString("0.0") == "1.0" && ___text.text == RDString.Get("levelSelect.SpeedTrial"))
                {
                    ___text.text = RDString.Get("status.getReady");
                }
            }
        }
        
        [TaggedPatch("HideSpeedTrialText")]
        [HarmonyPatch(typeof(scrCountdown), "ShowGetReady")]
        private static class ScrCountdownShowGetReady
        {
            private static void Postfix(Text ___text)
            {
                if (GCS.speedTrialMode && GCS.currentSpeedTrial.ToString("0.0") == "1.0")
                {
                    ___text.text = RDString.Get("status.getReady");
                }
            }
        }
        
    }
}