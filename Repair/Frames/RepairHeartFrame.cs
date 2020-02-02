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
    public class RepairHeartFrame : Frame
    {
    

        Entity manEntity, handEntity, leftHeartEntity, rightHeartEntity, heartEntity, cavity1Entity, cavity2Entity;

        bool mHeartRepaired = false;

        public RepairHeartFrame()
        {

        }

        public override void Init()
        {
            base.Init();
            var scene = Core.Scene;
            var man = scene.Content.Load<Texture2D>("man");
            manEntity = CreateEntity("manforsurgery");
            manEntity.AddComponent(new SpriteRenderer(man));
            manEntity.Transform.Position = new Vector2(400, 400);
            manEntity.Transform.SetScale(0.5f);

            var cavity1 = scene.Content.Load<Texture2D>("cavity");
            cavity1Entity = CreateEntity("cavity1");
            cavity1Entity.AddComponent(new SpriteRenderer(cavity1));
            cavity1Entity.Transform.Position = new Vector2(370, 260);
            cavity1Entity.Transform.SetScale(0.2f);



            var rightHeart = scene.Content.Load<Texture2D>("heartright");
            rightHeartEntity = CreateEntity("rightHeart");
            rightHeartEntity.AddComponent(new SpriteRenderer(rightHeart));
            rightHeartEntity.Transform.Position = new Vector2(371, 267);
            rightHeartEntity.Transform.SetScale(0.2f);

            var leftHeart = scene.Content.Load<Texture2D>("leftheart");
            leftHeartEntity = CreateEntity("leftHeart");
            leftHeartEntity.AddComponent(new SpriteRenderer(leftHeart));
            leftHeartEntity.Transform.Position = new Vector2(360, 277);
            leftHeartEntity.Transform.SetScale(0.2f);

            var heart = scene.Content.Load<Texture2D>("heart");
            heartEntity = CreateEntity("heart");
            heartEntity.AddComponent(new SpriteRenderer(heart));
            heartEntity.Transform.Position = new Vector2(400, 400);
            heartEntity.Transform.SetScale(0.2f);
            heartEntity.SetEnabled(false);

            var cavity2 = scene.Content.Load<Texture2D>("cavity2");
            cavity2Entity = CreateEntity("cavity2");
            cavity2Entity.AddComponent(new SpriteRenderer(cavity2));
            cavity2Entity.Transform.Position = new Vector2(370, 260);
            cavity2Entity.Transform.SetScale(0.2f);

            var hand = scene.Content.Load<Texture2D>("hand");
            handEntity = CreateEntity("hand");
            handEntity.AddComponent(new SpriteRenderer(hand));
            handEntity.Transform.Position = new Vector2(400, 400);
            handEntity.Transform.SetScale(0.05f);
            handEntity.SetEnabled(false);

            handEntity.AddComponent(new GrabberComponent());
            leftHeartEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
            rightHeartEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
            heartEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
        }

        public override void Update()
        {
            if (!mHeartRepaired)
            {
                TryToCompleteHeart();
            }
            else
            {
                ReceiveHeart();
            }
        }

        private void TryToCompleteHeart()
        {
            Vector2 difference = rightHeartEntity.Transform.Position - leftHeartEntity.Transform.Position;
            if( Math.Abs(difference.Y) < 20.0f && difference.X > 50.0f && difference.X < 100.0f)
            {
                heartEntity.SetEnabled(true);
                leftHeartEntity.SetEnabled(false);
                rightHeartEntity.SetEnabled(false);
                heartEntity.Transform.SetPosition(leftHeartEntity.Transform.Position + difference / 2.0f);
                handEntity.GetComponent<GrabberComponent>().LetGo();
                mHeartRepaired = true;
            }
        }

        public void ReceiveHeart()
        {
            if (heartEntity.GetComponent<GrabableComponent>().IsInsideRadius(cavity2Entity.Transform.Position))
            {
                OnFinish();
            }
        }
    }
}
