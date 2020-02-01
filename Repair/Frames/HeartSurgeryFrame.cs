using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Repair.Framework;

namespace Repair.Frames
{
    public class HeartSurgeryFrame : DrawFrame
    {

        enum SurgeryStage
        {
            MAN_PAN,
            CUT,
            TRANSITION_0,
            REMOVE_HEART,
            TRANSITION_1,
            PAIR_HEART,
            TRANSITION_2,
            PLACE_HEART
        }

        SurgeryStage stage = SurgeryStage.MAN_PAN;

        Entity manEntity, leftHeartEntity, rightHeartEntity, heartEntity, cavity1Entity, cavity2Entity, pupil1Entity, pupil2Entity;
        public HeartSurgeryFrame()
        {
            var scene = Core.Scene;
            var man = scene.Content.Load<Texture2D>("man");
            manEntity = CreateEntity("man");
            manEntity.AddComponent(new SpriteRenderer(man));
            manEntity.Transform.Position = new Vector2(400, -200);
            manEntity.Transform.SetScale(0.5f);

            var leftHeart = scene.Content.Load<Texture2D>("leftHeart");
            leftHeartEntity = CreateEntity("leftHeart");
            leftHeartEntity.AddComponent(new SpriteRenderer(leftHeart));
            leftHeartEntity.Transform.Position = new Vector2(400, 400);
            leftHeartEntity.Transform.SetScale(0.5f);

            var rightHeart = scene.Content.Load<Texture2D>("rightHeart");
            rightHeartEntity = CreateEntity("rightHeart");
            rightHeartEntity.AddComponent(new SpriteRenderer(rightHeart));
            rightHeartEntity.Transform.Position = new Vector2(400, 400);
            rightHeartEntity.Transform.SetScale(0.5f);

            var heart = scene.Content.Load<Texture2D>("heart");
            heartEntity = CreateEntity("heart");
            heartEntity.AddComponent(new SpriteRenderer(heart));
            heartEntity.Transform.Position = new Vector2(400, 400);
            heartEntity.Transform.SetScale(0.5f);

            var cavity1 = scene.Content.Load<Texture2D>("cavity");
            cavity1Entity = CreateEntity("cavity1");
            cavity1Entity.AddComponent(new SpriteRenderer(cavity1));
            cavity1Entity.Transform.Position = new Vector2(400, 400);
            cavity1Entity.Transform.SetScale(0.5f);

            var cavity2 = scene.Content.Load<Texture2D>("cavity2");
            cavity2Entity = CreateEntity("cavity2");
            cavity2Entity.AddComponent(new SpriteRenderer(cavity2));
            cavity2Entity.Transform.Position = new Vector2(400, 400);
            cavity2Entity.Transform.SetScale(0.5f);

            var pupil2 = scene.Content.Load<Texture2D>("pupil2");
            pupil2Entity = CreateEntity("pupil2");
            pupil2Entity.AddComponent(new SpriteRenderer(pupil2));
            pupil2Entity.Transform.Position = new Vector2(400, 400);
            pupil2Entity.Transform.SetScale(0.5f);

            var pupil1 = scene.Content.Load<Texture2D>("pupil1");
            pupil1Entity = CreateEntity("pupil1");
            pupil1Entity.AddComponent(new SpriteRenderer(pupil1));
            pupil1Entity.Transform.Position = new Vector2(400, 400);
            pupil1Entity.Transform.SetScale(0.5f);
        }

        public override void Update()
        {
            base.Update();
            switch(stage)
            {
                case SurgeryStage.MAN_PAN:
                    ManPan();
                    break;
            }
        }

        public void ManPan() { }

    }
}
