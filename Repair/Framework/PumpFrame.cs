using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Repair.Framework;

namespace Framework
{
    public class PumpFrame : Frame
    {
        public override string HintSpriteName => "controls_space";

        //private int 
        protected float mPressure = 0.0f;
        protected float mPumpPotential = 0.0f;

        protected float mPumpPotentialAcceleration = 50.0f;
        protected float mPumpPotentialStartSpeed = 15.0f;
        protected float mPumpPotentialSpeed = 15.0f;
        protected float mPumpPotentialLimit = 0.1f;
        protected float mPressureProportion = 0.0f;
        protected float mMaxPressure = 3.0f;

        private bool mReleasing = false;

        protected bool mGauge = false;

        private Entity gaugeEntity, pointerEntity, pointerPivotEntity, handEntity; 

        public PumpFrame()
        {
        }

        public override void Init()
        {
            base.Init();

            if(mGauge)
            {
                Vector2 offset = new Vector2(-400, -50);

                var gauge = Core.Scene.Content.Load<Texture2D>("gauge");
                gaugeEntity = CreateEntity("gauge");
                gaugeEntity.AddComponent(new SpriteRenderer(gauge));
                gaugeEntity.Transform.Position = new Vector2(849, 161) + offset;
                gaugeEntity.Transform.SetScale(0.15f);

                var pointer = Core.Scene.Content.Load<Texture2D>("pointer");
                pointerEntity = CreateEntity("pointer");
                pointerEntity.AddComponent(new SpriteRenderer(pointer));
                pointerEntity.Transform.Position = new Vector2(0,-75);
                pointerEntity.Transform.SetScale(0.15f);

                pointerPivotEntity = CreateEntity("pointerPivot");
                pointerPivotEntity.Transform.SetPosition(new Vector2(852, 235) + offset);
                pointerEntity.Transform.SetParent(pointerPivotEntity.Transform);
                pointerPivotEntity.Transform.SetRotationDegrees(-90);

                var hand = Core.Scene.Content.Load<Texture2D>("hand");
                handEntity = CreateEntity("hand");
                handEntity.AddComponent(new SpriteRenderer(hand));
                handEntity.Transform.Position = new Vector2(400, 400);
                handEntity.Transform.SetScale(0.05f);
                handEntity.SetEnabled(false);

                handEntity.AddComponent(new GrabberComponent());
                pointerEntity.AddComponent(new GrabableComponent(80.0f, handEntity));
                gaugeEntity.AddComponent(new GrabableComponent(80.0f, handEntity));

                mPumpPotentialSpeed = mPumpPotentialStartSpeed;
            }

        }

        public override void Update()
        {
            base.Update();
            if(Input.IsKeyPressed(Keys.Space))
            {
                Pump();
            }
            if(mReleasing)
            {
                float pumpAmount = 0.001f * 30.0f * mPumpPotentialSpeed;
                mPumpPotential -= pumpAmount;
                mPressure += pumpAmount;

                if (mPumpPotential < 0.0f)
                {
                    mReleasing = false;
                    mPumpPotential = 0.0f; 
                }
            }
            else
            {
                mPumpPotentialSpeed += 0.001f * mPumpPotentialAcceleration;
                mPumpPotential += 0.001f * mPumpPotentialSpeed;
                mPumpPotential = Math.Min(mPumpPotential, 1.0f);
            }
            mPressureProportion = (float)Math.Min(mMaxPressure, mPressure) / mMaxPressure;
            if(mGauge)
            {
                pointerPivotEntity.Transform.SetRotationDegrees(-90 + 180 * mPressureProportion);
            }


            if(mPressureProportion.Equals(1.0f))
            {
                OnFinish();
            }
        }

        public void Pump()
        { 
            mPumpPotentialSpeed = mPumpPotentialStartSpeed;
            mReleasing = true;
        }
    }
}
