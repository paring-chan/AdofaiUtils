using UnityEngine;

namespace AdofaiUtils.Utils
{
    internal static class SpriteUtilities
    {
        public static Sprite FromTexture2D(Texture2D texture)
        {
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        }
    }
}