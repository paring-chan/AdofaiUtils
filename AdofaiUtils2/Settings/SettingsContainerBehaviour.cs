using System;
using AdofaiUtils2.Utils;
using UnityEngine;

namespace AdofaiUtils2.Settings
{
    public class SettingsContainerBehaviour : BaseBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingsUI.Instance.Container.SetActive(false);
                Utils.Utils.SaveConfig();
                Utils.Utils.SettingsOpen = false;
                scrController.instance.paused = false;
            }
        }
    }
}