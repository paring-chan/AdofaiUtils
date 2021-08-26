using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using AdofaiUtils2.Settings;
using AdofaiUtils2.UI;
using AdofaiUtils2.Utils.attribute;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AdofaiUtils2.Utils
{
    public static class Utils
    {
        public static bool SettingsOpen;

        private static readonly string SettingsPath = Path.Combine("Mods", "AdofaiUtils2", "Config");

        public static readonly Dictionary<Tweak, TweakSettings> SettingsMap = new Dictionary<Tweak, TweakSettings>();

        public static readonly Dictionary<Tweak, GameObject> TweakObjects = new Dictionary<Tweak, GameObject>();
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
                TweakObjects[t] = tweak;
                var tweakVR = tweak.AddComponent<VerticalLayoutGroup>();
                tweakVR.childForceExpandHeight = false;
                tweakVR.childControlWidth = true;
                tweakVR.spacing = 10.0f;
                tweak.AddComponent<LayoutElement>();
                tweak.transform.SetParent(SettingsUI.Instance.Content.transform);
                var toggle = Object.Instantiate(UIFactory.Toggle, tweak.transform);
                var tt = toggle.transform.GetChild(1).gameObject.GetComponent<Text>();
                toggle.AddComponent<LayoutElement>().preferredHeight = 50.0f;
                var toggleC = toggle.GetComponent<UnityEngine.UI.Toggle>();

                toggleC.isOn = settings.enabled;

                toggleC.onValueChanged.AddListener(arg0 =>
                {
                    if (settings.enabled != arg0)
                    {
                        settings.enabled = arg0;
                        if (settings.enabled)
                        {
                            EnableTweak(t, attr.PatchesType);
                        }
                        else
                        {
                            DisableTweak(t, attr.PatchesType);
                        }
                    }
                });

                var content = Object.Instantiate(UIFactory.Panel, tweak.transform);
                content.AddComponent<LayoutElement>();
                var layout = content.AddComponent<VerticalLayoutGroup>();
                layout.padding = new RectOffset(30, 30, 30, 30);
                if (!settings.enabled)
                {
                    content.SetActive(false);
                }

                tt.text = attr.Name;

                foreach (var field in from field in st.GetFields()
                    where field.GetCustomAttribute<attribute.Settings.DoNotRender>() == null
                    select field)
                {
                    if (field.FieldType == typeof(string))
                    {
                        MelonLogger.Msg($"{field.Name} - STRING");
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        var ftoggle = Object.Instantiate(UIFactory.Toggle, content.transform);
                        ftoggle.AddComponent<LayoutElement>().preferredHeight = 50.0f;
                        var ftt = ftoggle.transform.GetChild(1).gameObject.GetComponent<Text>();
                        ftoggle.AddComponent<LayoutElement>().preferredHeight = 50.0f;
                        var ftoggleC = ftoggle.GetComponent<UnityEngine.UI.Toggle>();
                        var l = field.GetCustomAttribute<attribute.Settings.Label>();
                        ftt.text = l != null ? l.text : field.Name;
                        ftoggleC.isOn = (bool)field.GetValue(settings);
                        ftoggleC.onValueChanged.AddListener(arg0 =>
                        {
                            field.SetValue(settings, arg0);
                            t.OnConfigUpdate();
                        });
                        var tp = field.GetCustomAttribute<attribute.Settings.PatchTagByConfig>();
                        if (tp != null)
                        {
                            ftoggleC.onValueChanged.AddListener(arg0 =>
                            {
                                if (arg0)
                                {
                                    AdofaiUtils2.instance.HarmonyInstance.TaggedPatch(tp.tag);
                                }
                                else
                                {
                                    AdofaiUtils2.instance.HarmonyInstance.TaggedUnPatch(tp.tag);
                                }
                            });
                            AdofaiUtils2.instance.HarmonyInstance.TaggedPatch(tp.tag);
                        }
                    }
                }

                if (settings.enabled)
                {
                    EnableTweak(t, attr.PatchesType);
                }
            }
        }

        private static void DisableTweak(Tweak t, Type patchesType)
        {
            try
            {
                var obj = TweakObjects[t];
                obj.transform.GetChild(1).gameObject.SetActive(false);
                var subclasses =
                    from type in patchesType.GetTypeInfo().DeclaredNestedTypes
                    where type.GetCustomAttribute<HarmonyPatch>() != null
                    select type;
                foreach (var subclass in subclasses)
                {
                    var meta = subclass.GetCustomAttribute<HarmonyPatch>();
                    var orig = meta.info.method;
                    foreach (var methodInfo in subclass.GetMethods())
                    {
                        AdofaiUtils2.instance.HarmonyInstance.Unpatch(orig, methodInfo);
                    }

                    MelonLogger.Msg($"Unpatched: {subclass.FullName}");
                }

                t.OnDisable();
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Unpatch Error: {e}");
                throw;
            }
        }

        private static void EnableTweak(Tweak t, Type patchesType)
        {
            try
            {
                var obj = TweakObjects[t];
                obj.transform.GetChild(1).gameObject.SetActive(true);
                var subclasses =
                    from type in patchesType.GetTypeInfo().DeclaredNestedTypes
                    where type.GetCustomAttribute<HarmonyPatch>() != null &&
                          type.GetCustomAttribute<TaggedPatch>() == null // 태그가 붙어있는 패치는 자동으로 패치하지않게 만들기
                    select type;
                foreach (var subclass in subclasses)
                {
                    AdofaiUtils2.instance.HarmonyInstance.CreateClassProcessor(subclass).Patch();
                    MelonLogger.Msg($"Patched: {subclass.FullName}");
                }

                t.OnEnable();
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Patch Error: {e}");
                throw;
            }
        }

        public static void SaveConfig()
        {
            foreach (var tweak in TweakList)
            {
                var path = Path.Combine(SettingsPath, $"{tweak.GetType().FullName}.xml");
                var settings = SettingsMap[tweak];
                Directory.CreateDirectory(SettingsPath);
                using (var writer = new StreamWriter(path))
                {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
        }

        private static IEnumerable<Type> GetTweakList(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(AddTweak), true).Length > 0)
                {
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
    }
}