using AdofaiUtils2.Settings;
using AdofaiUtils2.UI;
using UnityEngine;

namespace AdofaiUtils2.Utils
{
    public class BaseBehaviour : MonoBehaviour
    {
        public SettingsManager settings => SettingsManager.Instance;
        public UIManager ui => UIManager.Instance;
    }
}