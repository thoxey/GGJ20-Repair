using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Frames
{
    public class MenuFrame : Frame
    {
        private Entity play;

        public MenuFrame()
        {

        }
        public override void Init()
        {
            base.Init();

            var titleTexture = Core.Scene.Content.Load<Texture2D>("title_repair");
            var titleEntity = CreateEntity("bg");
            var spriteRenderer = new SpriteRenderer(titleTexture);
            titleEntity.AddComponent(spriteRenderer);

            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            titleEntity.Transform.Position = new Vector2(width / 2, (height / 2) - 50);
            titleEntity.Transform.Scale = new Vector2(0.35f);

            var playTexture = Core.Scene.Content.Load<Texture2D>("play");
            play = CreateEntity("play");
            play.AddComponent(new SpriteRenderer(playTexture));
            play.Transform.Position = new Vector2(Core.GraphicsDevice.Viewport.Width / 2, (Core.GraphicsDevice.Viewport.Height / 4) * 2.0f) + new Vector2(0, 230);
            play.Transform.SetScale(0.3f);
        }

        public override void Update()
        {
            base.Update();
            if(Input.LeftMouseButtonDown || Input.IsKeyDown(Keys.Space))
            {
                play.Transform.SetScale(0.26f);
                OnFinish();
            }
        }

    }
}
