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
        private Entity plungerEntity, toiletEntity, waterSpawnerEntity;//handEntity;

        private Vector2 plungerStartPosition = new Vector2(615, 325);
        public PlungeToiletFrame()
        {
        }

        public override void Init()
        {
            mGauge = false;
            mPumpPotentialStartSpeed = 40.0f;
            mMaxPressure = 11.0f;
            base.Init();

            var scene = Core.Scene;

            var toilet = scene.Content.Load<Texture2D>("toilet");
            toiletEntity = CreateEntity("toilet");
            toiletEntity.AddComponent(new SpriteRenderer(toilet));
            toiletEntity.Transform.SetPosition(new Vector2(546, 516));
            toiletEntity.Transform.SetScale(0.2f);
            toiletEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.9f);

            var plunger = scene.Content.Load<Texture2D>("plunger");
            plungerEntity = CreateEntity("plunger");
            plungerEntity.AddComponent(new SpriteRenderer(plunger));
            plungerEntity.Transform.SetPosition(plungerStartPosition);
            plungerEntity.Transform.SetScale(0.2f);
            plungerEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.5f);


            waterSpawnerEntity = CreateEntity("waterSpawner");
            waterSpawnerEntity.Transform.Position = new Vector2(610,471);
            var component = new ParticleSpriteSpawner();
            List<string> spriteNames = new List<string>();
            spriteNames.Add("drop0");
            spriteNames.Add("drop1");
            spriteNames.Add("drop2");
            spriteNames.Add("drop3");
            component.InitParticleSystem(ShouldSpawnWater, spriteNames, 1.0f, new Vector2(0.0f, -10000000.0f), new Vector2(5000000.0f, 5.0f),0.15f,180,0.0f);
            waterSpawnerEntity.AddComponent(component);

        }
        public override void Update()
        {
            base.Update();
            plungerEntity.Transform.SetPosition(new Vector2(plungerStartPosition.X, plungerStartPosition.Y - 200.0f*mPumpPotential));
        }

        public bool ShouldSpawnWater()
        {
            return mReleasing;
        }
    }
}
