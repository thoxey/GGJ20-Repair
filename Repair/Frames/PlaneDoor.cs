﻿using System;
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
            var doorTexture = Core.Scene.Content.Load<Texture2D>("newdoor");
            door = CreateEntity("door");
            door.AddComponent(new SpriteRenderer(doorTexture));
            var width = Core.GraphicsDevice.Viewport.Width;
            var height = Core.GraphicsDevice.Viewport.Height;
            door.Transform.Position = new Vector2(width * 0.4f, height - 180);
            door.Transform.SetScale(0.3f);

            SetUpDrawArea(.65f, new Vector2(185, 295), new Vector2(840, 335));
        }

        private void SetupPlaner()
        {
            var planerTexture = Core.Scene.Content.Load<Texture2D>("planer");
            planer = CreateEntity("door");
            planer.AddComponent(new SpriteRenderer(planerTexture));
            planer.Transform.SetScale(0.08f);
            var component = new Framework.ParticleSpriteSpawner();
            List<string> spriteNames = new List<string>();
            spriteNames.Add("curl0");
            spriteNames.Add("curl1");
            spriteNames.Add("curl2");
            spriteNames.Add("curl3");
            spriteNames.Add("curl4");
            spriteNames.Add("curl5");
            component.InitParticleSystem(ShouldSpawnWater, spriteNames, 1.0f, new Vector2(-5000000.0f, -5000000.0f), new Vector2(5000000.0f, 5.0f), 0.15f, 180, 0.0f);
            planer.AddComponent(component);
        }

        public bool ShouldSpawnWater()
        {
            return pressing;
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
