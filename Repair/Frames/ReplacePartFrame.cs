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
    public class ReplacePartFrame : Frame
    {


        Entity computerEntity, brokenPartEntity, newPartEntity, handEntity, computerLidEntity;

        bool mBrokenPartRemoved = false;
        bool mNewPartInstalled = false;
        bool mLidInstalled = false;

        public ReplacePartFrame()
        {

        }

        public override void Init()
        {
            base.Init();
            var scene = Core.Scene;
            var computer = scene.Content.Load<Texture2D>("bigcomputer");
            computerEntity = CreateEntity("computer");
            computerEntity.AddComponent(new SpriteRenderer(computer));
            computerEntity.Transform.Position = new Vector2(415, 353);
            computerEntity.Transform.SetScale(0.2f);

            var brokenPart = scene.Content.Load<Texture2D>("brokenpart");
            brokenPartEntity = CreateEntity("brokenpart");
            brokenPartEntity.AddComponent(new SpriteRenderer(brokenPart));
            brokenPartEntity.Transform.Position = new Vector2(466, 398);
            brokenPartEntity.Transform.SetScale(0.2f);
            //brokenPartEntity.Transform.SetRotationDegrees(-20);

            var newPart = scene.Content.Load<Texture2D>("newpart");
            newPartEntity = CreateEntity("newpart");
            newPartEntity.AddComponent(new SpriteRenderer(newPart));
            newPartEntity.Transform.Position = new Vector2(900, 400);
            newPartEntity.Transform.SetScale(0.2f);
            //newPartEntity.Transform.SetRotationDegrees(-20);

            var computerLid = scene.Content.Load<Texture2D>("computerlid");
            computerLidEntity = CreateEntity("computerlid");
            computerLidEntity.AddComponent(new SpriteRenderer(computerLid));
            computerLidEntity.Transform.Position = new Vector2(415, 353);
            computerLidEntity.Transform.SetScale(0.2f);

            var hand = scene.Content.Load<Texture2D>("hand");
            handEntity = CreateEntity("hand");
            handEntity.AddComponent(new SpriteRenderer(hand));
            handEntity.Transform.Position = new Vector2(400, 400);
            handEntity.Transform.SetScale(0.05f);
            handEntity.SetEnabled(false);

            handEntity.AddComponent(new GrabberComponent());
            computerLidEntity.AddComponent(new GrabableComponent(110.0f, handEntity, 5));
            newPartEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
            brokenPartEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
        }

        public override void Update()
        {
            mBrokenPartRemoved = !brokenPartEntity.GetComponent<GrabableComponent>().IsInsideRadius(computerEntity.Transform.Position);
            mLidInstalled = computerLidEntity.GetComponent<GrabableComponent>().IsInsideRadius(computerEntity.Transform.Position);
            mNewPartInstalled = newPartEntity.GetComponent<GrabableComponent>().IsInsideRadius(computerEntity.Transform.Position);
            if (mBrokenPartRemoved && mNewPartInstalled)
            {
                OnFinish();
            }
        }
    }
}
