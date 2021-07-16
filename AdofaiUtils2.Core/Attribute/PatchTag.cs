using System;

namespace AdofaiUtils2.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PatchTag
    {
        public string Name { get; set; }

        public PatchTag(string tag)
        {
            Name = tag;
        }
    }
}