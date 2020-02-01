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

        bool heartRepaired = false;

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

            var cavity2 = scene.Content.Load<Texture2D>("cavity2");
            cavity2Entity = CreateEntity("cavity2");
            cavity2Entity.AddComponent(new SpriteRenderer(cavity2));
            cavity2Entity.Transform.Position = new Vector2(370, 260);
            cavity2Entity.Transform.SetScale(0.2f);

            var leftHeart = scene.Content.Load<Texture2D>("leftheart");
            leftHeartEntity = CreateEntity("leftHeart");
            leftHeartEntity.AddComponent(new SpriteRenderer(leftHeart));
            leftHeartEntity.Transform.Position = new Vector2(400, 400);
            leftHeartEntity.Transform.SetScale(0.2f);
            leftHeartEntity.AddComponent(new BoxCollider());

            var rightHeart = scene.Content.Load<Texture2D>("heartright");
            rightHeartEntity = CreateEntity("rightHeart");
            rightHeartEntity.AddComponent(new SpriteRenderer(rightHeart));
            rightHeartEntity.Transform.Position = new Vector2(400, 400);
            rightHeartEntity.Transform.SetScale(0.2f);

            var hand = scene.Content.Load<Texture2D>("hand");
            handEntity = CreateEntity("hand");
            handEntity.AddComponent(new SpriteRenderer(hand));
            handEntity.Transform.Position = new Vector2(400, 400);
            handEntity.Transform.SetScale(0.2f);
            handEntity.SetEnabled(false);
            handEntity.AddComponent(new BoxCollider());

            //var heart = scene.Content.Load<Texture2D>("heart");
            //heartEntity = CreateEntity("heart");
            //heartEntity.AddComponent(new SpriteRenderer(heart));
            //heartEntity.Transform.Position = new Vector2(400, 400);
            //heartEntity.Transform.SetScale(0.2f);


        }

        public override void Update()
        {
            handEntity.Transform.SetPosition(Input.MousePosition);
            CollisionResult result;
            if (handEntity.GetComponent<Collider>().Overlaps(leftHeartEntity.GetComponent<Collider>()))
            {
                handEntity.SetEnabled(true);
            }
            else
            {
                handEntity.SetEnabled(false);
            }

        }

        public void ManPan()
        {

        }

       

    }
}
