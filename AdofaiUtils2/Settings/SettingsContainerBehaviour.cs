using System;
using AdofaiUtils2.Utils;
using MelonLoader;

namespace AdofaiUtils2.Settings
{
    public class SettingsContainerBehaviour : BaseBehaviour
    {
        private void Awake()
        {
            transform.SetParent(ui.CanvasRoot.transform);
            MelonLogger.Msg("와아앙");
        }
    }
}