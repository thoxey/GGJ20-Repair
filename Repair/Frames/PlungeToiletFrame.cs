using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Repair.Framework;
using Framework;

namespace Repair.Frames
{
    public class PlungeToiletFrame : PumpFrame
    {
        private Entity plungerEntity, toiletEntity, handEntity;

        private Vector2 plungerStartPosition = new Vector2(615, 325);
        public PlungeToiletFrame()
        {
        }

        public override void Init()
        {
            mGauge = false;
            mPumpPotentialStartSpeed = 40.0f;
            mMaxPressure = 6.0f;
            base.Init();

            var scene = Core.Scene;

            var toilet = scene.Content.Load<Texture2D>("toilet");
            toiletEntity = CreateEntity("toilet");
            toiletEntity.AddComponent(new SpriteRenderer(toilet));
            toiletEntity.Transform.SetPosition(new Vector2(546, 516));
            toiletEntity.Transform.SetScale(0.2f);

            var plunger = scene.Content.Load<Texture2D>("plunger");
            plungerEntity = CreateEntity("plunger");
            plungerEntity.AddComponent(new SpriteRenderer(plunger));
            plungerEntity.Transform.SetPosition(plungerStartPosition);
            plungerEntity.Transform.SetScale(0.2f);

            //var hand = scene.Content.Load<Texture2D>("hand");
            //handEntity = CreateEntity("hand");
            //handEntity.AddComponent(new SpriteRenderer(hand));
            //handEntity.Transform.Position = new Vector2(400, 400);
            //handEntity.Transform.SetScale(0.05f);
            //handEntity.SetEnabled(false);

            //handEntity.AddComponent(new GrabberComponent());
            //toiletEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
            //plungerEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
        }
        public override void Update()
        {
            base.Update();
           plungerEntity.Transform.SetPosition(new Vector2(plungerStartPosition.X, plungerStartPosition.Y - 200.0f*mPumpPotential));
        }
    }
}
