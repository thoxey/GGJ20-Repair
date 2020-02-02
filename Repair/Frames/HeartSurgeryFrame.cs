using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Repair.Framework;

namespace Frames
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

        Entity manEntity, surgeryLineEntity, scalpelEntity, cavity1Entity, cavity2Entity, pupil1Entity, pupil2Entity;
        int ticks1 = 0;
        int ticks2 = 23;
        Vector2 pupil2point = new Vector2(510, 300);
        Vector2 pupil1point = new Vector2(320, 300);
        float pupilModifier = 8.0f;

        public HeartSurgeryFrame()
        {

        }

        public override void Init()
        {
            base.Init();
            var scene = Core.Scene;

            var man = scene.Content.Load<Texture2D>("man");
            manEntity = CreateEntity("manforsurgery");
            manEntity.AddComponent(new SpriteRenderer(man));
            manEntity.Transform.Position = new Vector2(400, 1000);
            manEntity.Transform.SetScale(0.5f);
            manEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.9f);

            var pupil2 = scene.Content.Load<Texture2D>("pupil2");
            pupil2Entity = CreateEntity("pupil2");
            pupil2Entity.AddComponent(new SpriteRenderer(pupil2));
            pupil2Entity.Transform.Position = pupil2point;
            pupil2Entity.Transform.SetScale(0.2f);

            var pupil1 = scene.Content.Load<Texture2D>("pupil1");
            pupil1Entity = CreateEntity("pupil1");
            pupil1Entity.AddComponent(new SpriteRenderer(pupil1));
            pupil1Entity.Transform.Position = pupil1point;
            pupil1Entity.Transform.SetScale(0.2f);

            var surgeryLine = scene.Content.Load<Texture2D>("surgeryline");
            surgeryLineEntity = CreateEntity("surgeryline");
            surgeryLineEntity.AddComponent(new SpriteRenderer(surgeryLine));
            surgeryLineEntity.Transform.Position = new Vector2(370, 860);
            surgeryLineEntity.Transform.SetScale(0.2f);
            surgeryLineEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.2f);

            var scalpel = scene.Content.Load<Texture2D>("surgeonknife");
            scalpelEntity = CreateEntity("surgeonknife");
            scalpelEntity.AddComponent(new SpriteRenderer(scalpel));
            scalpelEntity.Transform.SetScale(0.1f);
            scalpelEntity.Enabled = false;
            scalpelEntity.AddComponent<Framework.ParticleSpriteSpawner>();
            List<string> spriteNames = new List<string>();
            //spriteNames.Add("curl0");
            //spriteNames.Add("curl1");
            //spriteNames.Add("curl2");
            //spriteNames.Add("curl3");
            //spriteNames.Add("curl4");
            //spriteNames.Add("curl5");
            spriteNames.Add("drop0");
            spriteNames.Add("drop1");
            spriteNames.Add("drop2");
            spriteNames.Add("drop3");

            scalpelEntity.GetComponent<Framework.ParticleSpriteSpawner>().InitParticleSystem(ShouldSpawnBlood, spriteNames, 1.0f, new Vector2(10000000, 0), new Vector2(0, 5000000), 0.1f, 180, 0.0f);
        }

        public override void Update()
        {
            base.Update();
            switch(stage)
            {
                case SurgeryStage.MAN_PAN:
                    ManPan();
                    break;
                default:
                    scalpelEntity.Position = Input.MousePosition + new Vector2(15, -45);
                    scalpelEntity.Enabled = IsInZone;
                    break;
            }
        }

        public bool ShouldSpawnBlood()
        {
            return pressing;
        }


        public void ManPan() {
            Vector2 translate = new Vector2(0,-5.0f);
            manEntity.Transform.SetPosition(manEntity.Transform.Position + translate);
            surgeryLineEntity.Transform.SetPosition(surgeryLineEntity.Transform.Position + translate);
            ticks1++;
            ticks2--;
            Vector2 translate2 = new Vector2((float)Math.Sin(ticks1/2),(float) Math.Cos(ticks1/2));
            Vector2 translate3 = new Vector2((float)Math.Sin(ticks2/2), (float)Math.Cos(ticks2/2));
            translate2 *= pupilModifier;
            translate3 *= pupilModifier;
            pupil1point += translate;
            pupil2point += translate;
            pupil1Entity.Transform.SetPosition(pupil1point + translate2);
            pupil2Entity.Transform.SetPosition(pupil2point + translate3);
            if (manEntity.Transform.Position.Y < 400) {
                manEntity.Transform.SetPosition(manEntity.Transform.Position.X, 400);
                stage = SurgeryStage.CUT;
                SetUpDrawArea(0.4f, new Vector2(318, 82), new Vector2(413, 418));
            }
        }

        protected override void AreaFilled()
        {
            base.AreaFilled();
            OnFinish();
            //cavity1Entity.SetEnabled(true);
            //cavity2Entity.SetEnabled(true);
            //surgeryLineEntity.SetEnabled(false);

        }

        public override void OnNodeAdded(Vector2 NodePos)
        {
            //if(splatterCount % 2 == 0)
            //{
            //    var splatterTexture = GetRandomBloodSplatter();
            //    var splat = CreateEntity("Splatter" + (splatterCount++).ToString());
            //    splat.AddComponent(new SpriteRenderer(splatterTexture));
            //    splat.Transform.Position = NodePos;
            //    splat.Transform.SetScale(0.2f);
            //}
            //else
            //{
            //    splatterCount++;
            //}
        }

        List<string> masterSplatters = new List<string> { "drop0", "drop1", "drop2", "drop3" };
        List<string> splatters = new List<string> { "drop0", "drop1", "drop2", "drop3"};

        private Texture2D GetRandomBloodSplatter()
        {
            string splatter;
            if (splatters.Count > 1)
            {
                splatters.Shuffle();
                splatter = splatters[0];
                splatters.RemoveAt(0);
            }
            else
            {
                splatter = splatters[0];
                splatters = masterSplatters;
            }
            return Core.Scene.Content.Load<Texture2D>(splatter);
        }

        private static System.Random rng = new System.Random();

        public void Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
