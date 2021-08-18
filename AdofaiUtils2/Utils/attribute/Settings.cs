using System;

namespace AdofaiUtils2.Utils.attribute
{
    public static class Settings
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class Label : Attribute
        {
            public string text;
            
            public Label(string text)
            {
                this.text = text;
            }
        }
        
        [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
        public class PatchTagByConfig : Attribute
        {
            public string tag;
            
            public PatchTagByConfig(string text)
            {
                this.tag = text;
            }
        }
        
        [AttributeUsage(AttributeTargets.Field)]
        public class DoNotRender : Attribute
        {}
    }
}