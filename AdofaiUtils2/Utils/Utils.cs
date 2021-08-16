using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using AdofaiUtils2.Settings;
using AdofaiUtils2.UI;
using AdofaiUtils2.Utils.attribute;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.Utils
{
    public static class Utils
    {
        private static readonly string SettingsPath = Path.Combine("Mods", "AdofaiUtils2", "Config");
        
        public static readonly Dictionary<Tweak, TweakSettings> SettingsMap = new Dictionary<Tweak, TweakSettings>();

        public static Dictionary<Tweak, GameObject> TweakObjects = new Dictionary<Tweak, GameObject>();
        public static readonly List<Tweak> TweakList = new List<Tweak>();

        public static void LoadTweaks()
        {
            foreach (var type in GetTweakList(Assembly.GetExecutingAssembly()))
            {
                var attr = type.GetCustomAttribute<AddTweak>();
                var tweak = (Tweak)Activator.CreateInstance(type);
                LoadConfig(tweak, attr.SettingsType);
                TweakList.Add(tweak);
            }
        }

        public static void RenderTweakSettings()
        {
            foreach (var t in TweakList)
            {
                var settings = SettingsMap[t];
                var st = settings.GetType();
                var attr = t.GetType().GetCustomAttribute<AddTweak>();
                var tweak = new GameObject("Tweak");
                var tweakVR = tweak.AddComponent<VerticalLayoutGroup>();
                tweakVR.childForceExpandHeight = false;
                var le = tweak.AddComponent<LayoutElement>();
                tweak.transform.SetParent(SettingsUI.Instance.Content.transform);
                var toggle = Object.Instantiate(UIFactory.Toggle, tweak.transform);
                var tt = toggle.transform.GetChild(1).gameObject.GetComponent<Text>();
                toggle.AddComponent<LayoutElement>().preferredHeight = 50.0f;
                tt.text = attr.Name;
            }
        }
        
        private static IEnumerable<Type> GetTweakList(Assembly assembly) {
            foreach(Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(AddTweak), true).Length > 0) {
                    yield return type;
                }
            }
        }

        private static void LoadConfig(Tweak tweak, Type type)
        {
            var path = Path.Combine(SettingsPath, $"{tweak.GetType().FullName}.xml");
            {
                try
                {
                    TweakSettings result;
                    if (File.Exists(path))
                    {
                        using (var stream = File.OpenRead(path))
                        {
                            var serializer = new XmlSerializer(type);
                            result = (TweakSettings)serializer.Deserialize(stream);
                        }
                    }
                    else
                    {
                        result = (TweakSettings)Activator.CreateInstance(type);
                    }
                    SettingsMap[tweak] = result;
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"[{type.FullName}] Settings loading failed: {e}");
                }
            }
        }
        
        public static IEnumerable<Type> GetEnumerableOfType<T>() where T : class
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in 
                Assembly.GetAssembly(typeof(T)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add(type);
            }
            objects.Sort();
            return objects;
        }
    }
}