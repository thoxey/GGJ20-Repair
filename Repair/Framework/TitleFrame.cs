using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Framework
{
    public class TitleFrame : TimedFrame
    {
        protected string titleAssetName;
        private Entity titleEntity;

        private float scalar;

        public TitleFrame(string titleAsset, float scaleAdjustment = 0.35f)
        {
            titleAssetName = titleAsset;
            scalar = scaleAdjustment;
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
            titleEntity.Transform.Position = new Vector2(width / 2, (height / 2) - 50);
            titleEntity.Transform.Scale *= scalar;
        }

        public override void Update()
        {
            base.Update();
            //if (Input.IsKeyDown(Keys.Space))
            //{
            //    Timeout -= OnTimeout;
            //    OnFinish();
            //}
        }
    }
}
