using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Repair.Framework;

namespace Frames
{
    public class PlaneDoor : DrawFrame
    {
        public override void Init()
        {
            base.Init();

            var doorTexture = Core.Scene.Content.Load<Texture2D>("door");
            var door = CreateEntity("door");
            door.AddComponent(new SpriteRenderer(doorTexture));
            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            door.Transform.Position = new Vector2(width * 0.4f, height);
            door.Transform.SetScale(0.3f);

            SetUpDrawArea(.8f, new Vector2(185, 395), new Vector2(840, 435));
        }

        public override void AreaFilled()
        {
            base.AreaFilled();
            OnFinish();
        }

        public override void OnNodeAdded(Vector2 NodePos)
        {
        }
    }
}
