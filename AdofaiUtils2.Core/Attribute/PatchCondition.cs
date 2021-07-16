using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace AdofaiUtils2.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PatchCondition : HarmonyPatch
    {
        private static Dictionary<string, bool> _enables = new Dictionary<string, bool>();
        public string ClassName { get; set; }
        public string Id { get; set; }
        public string Method { get; set; }
        
        public Assembly Assembly { get; set; }

        public int MinVersion { get; set; }
        
        public int MaxVersion { get; set; }
        
        public bool IsEnabled {
            get {
                if (!_enables.ContainsKey(Id)) _enables[Id] = false;
                return _enables[Id];
            }
            set => _enables[Id] = value;
        }
        
        public PatchCondition(string id, string className, string method, int minVersion = -1, int maxVersion = -1, Assembly asm = null)
        {
            Id = id;
            ClassName = className;
            Assembly = asm == null ? Assembly.GetAssembly(typeof(ADOBase)) : asm;
            Method = method;
            MinVersion = minVersion;
            MaxVersion = maxVersion;
        }
    }
}