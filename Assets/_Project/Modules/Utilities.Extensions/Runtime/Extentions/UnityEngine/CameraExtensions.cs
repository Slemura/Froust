using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static Sprite TakeSnapshot(this Camera self, int width, int height)
        {
            Texture2D Render()
            {
                var renderTexture = RenderTexture.GetTemporary(width, height);
                var currentTexture = self.targetTexture;

                self.targetTexture = renderTexture;
                self.Render();
                self.targetTexture = currentTexture;

                RenderTexture.active = renderTexture;

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
                texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                texture.Apply();

                RenderTexture.active = null;

                RenderTexture.ReleaseTemporary(renderTexture);
                return texture;
            }

            var enabled = self.enabled;

            self.enabled = true;

            var texture = Render();

            self.enabled = enabled;

            var rect = new Rect(0, 0, texture.width, texture.height);

            var snapshot = Sprite.Create(texture, rect, Vector2.zero);
            return snapshot;
        }
    }
}