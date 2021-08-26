using AdofaiUtils2.Utils;
using HarmonyLib;
using UnityEngine.UI;

namespace AdofaiUtils2.Tweaks.Play
{
    public class PlayTweakPatches
    {
        [TaggedPatch("Tweaks.Play.Hide10")]
        [HarmonyPatch(typeof(scrController), "Update")]
        private static class ScrControllerUpdate
        {
            private static void Prefix(scrController __instance)
            {
                var scrController = __instance;
                if (scrController.txtCaption != null &&
                    GCS.speedTrialMode && GCS.currentSpeedRun.ToString("0.0") == "1.0")
                {
                    scrController.txtCaption.text = __instance.caption;
                }
            }
        }
        
        [TaggedPatch("AdofaiUtils2.Play.Hide10")]
        [HarmonyPatch(typeof(scrCountdown), "Update")]
        private static class ScrCountdownUpdate
        {
            private static void Postfix(Text ___text)
            {
                if (GCS.speedTrialMode && GCS.currentSpeedRun.ToString("0.0") == "1.0" && ___text.text == RDString.Get("levelSelect.SpeedTrial"))
                {
                    ___text.text = RDString.Get("status.getReady");
                }
            }
        }
        
        [TaggedPatch("AdofaiUtils2.Play.Hide10")]
        [HarmonyPatch(typeof(scrCountdown), "ShowGetReady")]
        private static class ScrCountdownShowGetReady
        {
            private static void Postfix(Text ___text)
            {
                if (GCS.speedTrialMode && GCS.currentSpeedRun.ToString("0.0") == "1.0")
                {
                    ___text.text = RDString.Get("status.getReady");
                }
            }
        }
    }
}