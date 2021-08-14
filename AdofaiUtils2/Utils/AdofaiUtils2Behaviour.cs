using UnityEngine;

namespace AdofaiUtils2.Utils
{
    public class AdofaiUtils2Behaviour : MonoBehaviour
    {
        internal static AdofaiUtils2Behaviour Instance { get; private set; }

        internal static void Setup()
        {
            var obj = new GameObject("AdofaiUtils2 Behaviour");
            GameObject.DontDestroyOnLoad(obj);
            obj.hideFlags |= HideFlags.HideAndDontSave;
            Instance = obj.AddComponent<AdofaiUtils2Behaviour>();
        }
    }
}