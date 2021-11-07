using System.IO;

namespace AdofaiUtils.Utils
{
    internal static class ResourceLoader
    {
        public static byte[] ReadFully(Stream input)
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