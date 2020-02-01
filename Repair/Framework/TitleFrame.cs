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

        public TitleFrame(string titleAsset)
        {
            titleAssetName = titleAsset;
        }

        public override void Init()
        {
            base.Init();

            var titleTexture = Core.Scene.Content.Load<Texture2D>(titleAssetName);
            titleEntity = CreateEntity(titleAssetName);
            var spriteRenderer = new SpriteRenderer(titleTexture);
            titleEntity.AddComponent(spriteRenderer);

            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            titleEntity.Transform.Position = new Vector2(width / 2, height / 2);
        }
    }
}
