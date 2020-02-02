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
        private Entity door, planer;

        public override void Init()
        {
            base.Init();
            SetupDoor();
            SetupPlaner();
        }

        private void SetupDoor()
        {
            var doorTexture = Core.Scene.Content.Load<Texture2D>("door");
            door = CreateEntity("door");
            door.AddComponent(new SpriteRenderer(doorTexture));
            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            door.Transform.Position = new Vector2(width * 0.4f, height);
            door.Transform.SetScale(0.3f);

            var planer = Core.Scene.Content.Load<Texture2D>("planer");
            var planerEntity = CreateEntity("planer");
            planerEntity.AddComponent(new SpriteRenderer(planer));
            planerEntity.Transform.Position = new Vector2(400, 400);
            planerEntity.Transform.SetScale(0.15f);
            planerEntity.AddComponent(new FollowMouseComponent());

            SetUpDrawArea(.65f, new Vector2(185, 295), new Vector2(840, 335));
        }

        private void SetupPlaner()
        {
            var planerTexture = Core.Scene.Content.Load<Texture2D>("planer");
            planer = CreateEntity("door");
            planer.AddComponent(new SpriteRenderer(planerTexture));
            planer.Transform.SetScale(0.08f);
        }

        public override void Update()
        {
            base.Update();
            planer.Enabled = IsInZone;
            planer.Transform.Position = Input.MousePosition + new Vector2(0, -30);
        }

        protected override void AreaFilled()
        {
            base.AreaFilled();
            OnFinish();
        }


        public override void OnNodeAdded(Vector2 NodePos)
        {
            door.Transform.Position += new Vector2(0f, 1f);
        }
    }
}
