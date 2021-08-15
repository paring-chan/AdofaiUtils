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
                gameObject.SetActive(false);
                scrController.instance.paused = false;
                scrController.instance.audioPaused = false;
                scrController.instance.enabled = true;
                Time.timeScale = 1.0f;
            }
        }
    }
}