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
    public class PumpTyreFrame : PumpFrame
    {
        private Entity tyreEntity, tyrePivotEntity, pumpPeddleEntity, pumpPeddlePivotEntity, pumpBaseEntity, footEntity, handEntity;

        private Vector2 legStartPosition = new Vector2(249, 209);
        public PumpTyreFrame()
        {
        }

        public override void Init()
        {
            mGauge = true;
            base.Init();

            var scene = Core.Scene;
            var tyre = scene.Content.Load<Texture2D>("tire");
            tyreEntity = CreateEntity("tire");
            tyreEntity.AddComponent(new SpriteRenderer(tyre));
            tyreEntity.Transform.Position = new Vector2(0, -150);
            tyreEntity.Transform.SetScale(0.2f);

            tyrePivotEntity = CreateEntity("tirePivot");
            tyrePivotEntity.Transform.SetPosition(new Vector2(867, 624));
            tyreEntity.Transform.SetParent(tyrePivotEntity.Transform);

            var pumpBase = scene.Content.Load<Texture2D>("pump2");
            pumpBaseEntity = CreateEntity("pumpBase");
            pumpBaseEntity.AddComponent(new SpriteRenderer(pumpBase));
            pumpBaseEntity.Transform.Position = new Vector2(408, 585);
            pumpBaseEntity.Transform.SetScale(0.2f);

            var pumpPeddle = scene.Content.Load<Texture2D>("pump1");
            pumpPeddleEntity = CreateEntity("pumpPeddle");
            pumpPeddleEntity.AddComponent(new SpriteRenderer(pumpPeddle));
            pumpPeddleEntity.Transform.Position = new Vector2(-126, -38);
            pumpPeddleEntity.Transform.SetScale(0.2f);

            pumpPeddlePivotEntity = CreateEntity("pumpPivot");
            pumpPeddlePivotEntity.Transform.SetPosition(new Vector2(529, 605));
            pumpPeddleEntity.Transform.SetParent(pumpPeddlePivotEntity.Transform);

            var foot = scene.Content.Load<Texture2D>("leg");
            footEntity = CreateEntity("leg");
            footEntity.AddComponent(new SpriteRenderer(foot));
            footEntity.Transform.SetPosition(legStartPosition);
            footEntity.Transform.SetScale(0.2f);
        }
        public override void Update()
        {
            base.Update();
            float degrees = 40 * mPumpPotential;
            pumpPeddlePivotEntity.Transform.SetRotationDegrees(degrees);
            footEntity.Transform.SetPosition( new Vector2(legStartPosition.X, legStartPosition.Y - 200.0f * (float)Math.Sin(Math.PI * degrees / 180.0)));
            tyrePivotEntity.Transform.SetScale(1.0f + mPressureProportion);
        }
    }
}
