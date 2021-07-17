using System;

namespace AdofaiUtils2.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PatchTag : System.Attribute
    {
        public string Name { get; set; }

        public PatchTag(string tag)
        {
            Name = tag;
        }
    }
}