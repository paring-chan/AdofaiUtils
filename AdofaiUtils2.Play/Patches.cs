using AdofaiUtils2.Core.Attribute;
using UnityEngine;
using UnityEngine.UI;

namespace AdofaiUtils2.Play
{
    internal static class Patches
    {
        [PatchTag("AdofaiUtils2.Play.Hide10")]
        [PatchCondition("AdofaiUtils2.Play.ScrControllerUpdate", "scrController", "Update")]
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
        
        [PatchTag("AdofaiUtils2.Play.Hide10")]
        [PatchCondition("AdofaiUtils2.Play.ScrCountdownUpdate", "scrCountdown", "Update")]
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
        
        [PatchTag("AdofaiUtils2.Play.Hide10")]
        [PatchCondition("AdofaiUtils2.Play.ScrCountdownShowGetReady", "scrCountdown", "ShowGetReady")]
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