using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Framework
{
    public class TitleFrame : TimedFrame
    {
        protected string titleAssetName;
        private Entity titleEntity;

        public TitleFrame(string titleAsset) : base()
        {
            titleAssetName = titleAsset;

            var titleTexture = Core.Scene.Content.Load<Texture2D>(titleAssetName);
            titleEntity = Core.Scene.CreateEntity(titleAssetName);
            var spriteRenderer = new SpriteRenderer(titleTexture);
            titleEntity.AddComponent(spriteRenderer);

            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            titleEntity.Transform.Position = new Vector2(width/2, height/2);
        }

        protected override void OnTimeout(ITimer timer)
        {
            titleEntity.Destroy();
        }
    }
}
