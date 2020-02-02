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
    public class ReplaceLightsFrame : Frame
    {


        Entity carEntity, brokenLightEntity, newLightEntity, handEntity;

        bool mBrokenLightRemoved = false;
        bool mNewLightInstalled = false;

        public ReplaceLightsFrame()
        {

        }

        public override void Init()
        {
            base.Init();
            var scene = Core.Scene;
            var car= scene.Content.Load<Texture2D>("carforlight");
            carEntity = CreateEntity("carforlight");
            carEntity.AddComponent(new SpriteRenderer(car));
            carEntity.Transform.Position = new Vector2(415, 353);
            carEntity.Transform.SetScale(0.2f);

            var brokenLight = scene.Content.Load<Texture2D>("brokenlight");
            brokenLightEntity = CreateEntity("brokenlight");
            brokenLightEntity.AddComponent(new SpriteRenderer(brokenLight));
            brokenLightEntity.Transform.Position = new Vector2(453, 368);
            brokenLightEntity.Transform.SetScale(0.17f);
            brokenLightEntity.Transform.SetRotationDegrees(-20);

            var newLight = scene.Content.Load<Texture2D>("fixedlight");
            newLightEntity = CreateEntity("fixedlight");
            newLightEntity.AddComponent(new SpriteRenderer(newLight));
            newLightEntity.Transform.Position = new Vector2(900, 400);
            newLightEntity.Transform.SetScale(0.17f);
            newLightEntity.Transform.SetRotationDegrees(-20);

            var hand = scene.Content.Load<Texture2D>("hand");
            handEntity = CreateEntity("hand");
            handEntity.AddComponent(new SpriteRenderer(hand));
            handEntity.Transform.Position = new Vector2(400, 400);
            handEntity.Transform.SetScale(0.05f);
            handEntity.SetEnabled(false);

            handEntity.AddComponent(new GrabberComponent());
            newLightEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
            brokenLightEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
        }

        public override void Update()
        {
            Vector2 difference = newLightEntity.Transform.Position - carEntity.Transform.Position;
            mBrokenLightRemoved = !brokenLightEntity.GetComponent<GrabableComponent>().IsInsideRadius(carEntity.Transform.Position);
            mNewLightInstalled = newLightEntity.GetComponent<GrabableComponent>().IsInsideRadius(carEntity.Transform.Position);
            if(mBrokenLightRemoved && mNewLightInstalled)
            {
                OnFinish();
            }
        }
    }
}
