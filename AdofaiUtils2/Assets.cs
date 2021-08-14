using System.IO;
using UnityEngine;

namespace AdofaiUtils2
{
    public class Assets
    {
        public static AssetBundle Bundle;
        
        public static void Init()
        {
            Bundle = AssetBundle.LoadFromMemory(ReadFully(
                typeof(AdofaiUtils2).Assembly.GetManifestResourceStream("AdofaiUtils2.Resources.assets.bundle")));
        }
        
        private static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[81920];
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }
    }
}