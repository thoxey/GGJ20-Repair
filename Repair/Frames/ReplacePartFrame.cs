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

        Vector2 computerPos;

        public ReplacePartFrame()
        {

        }

        public override void Init()
        {
            base.Init();
            var scene = Core.Scene;

            computerPos = new Vector2(640, 353);

            var computer = scene.Content.Load<Texture2D>("bigcomputer");
            computerEntity = CreateEntity("computer");
            computerEntity.AddComponent(new SpriteRenderer(computer));
            computerEntity.Transform.Position = computerPos;
            computerEntity.Transform.SetScale(0.25f);

            var brokenPart = scene.Content.Load<Texture2D>("brokenpart");
            brokenPartEntity = CreateEntity("brokenpart");
            brokenPartEntity.AddComponent(new SpriteRenderer(brokenPart));
            brokenPartEntity.Transform.Position = computerPos + new Vector2(41, 45);
            brokenPartEntity.Transform.SetScale(0.25f);
            //brokenPartEntity.Transform.SetRotationDegrees(-20);

            var newPart = scene.Content.Load<Texture2D>("newpart");
            newPartEntity = CreateEntity("newpart");
            newPartEntity.AddComponent(new SpriteRenderer(newPart));
            newPartEntity.Transform.Position = computerPos + new Vector2(-485, 47);
            newPartEntity.Transform.SetScale(0.25f);
            //newPartEntity.Transform.SetRotationDegrees(-20);

            var computerLid = scene.Content.Load<Texture2D>("computerlid");
            computerLidEntity = CreateEntity("computerlid");
            computerLidEntity.AddComponent(new SpriteRenderer(computerLid));
            computerLidEntity.Transform.Position = computerPos;
            computerLidEntity.Transform.SetScale(0.25f);

            var hand = scene.Content.Load<Texture2D>("hand");
            handEntity = CreateEntity("hand");
            handEntity.AddComponent(new SpriteRenderer(hand));
            handEntity.Transform.Position = computerPos + new Vector2(-15, 47);
            handEntity.Transform.SetScale(0.1f);
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
                OnInPosition();
            }
        }

        float timer = 0f;
        private void OnInPosition()
        {
            newPartEntity.Position = computerPos + new Vector2(41, 45);
            newPartEntity.GetComponent<GrabableComponent>().Enabled = false;
            if (timer < 1.0f)
            {
                timer += 0.03f;
                return;
            }

            OnFinish();
        }
    }
}
