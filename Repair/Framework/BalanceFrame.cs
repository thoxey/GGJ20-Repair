using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Framework
{
    public abstract class BalanceFrame : Frame
    {
        public BalanceFrame()
        {
        }

        Entity levelEntity, bubbleEntity;

        protected float angle = 45f;
        protected float speed = 0f;
        protected bool isAligned = false;

        private readonly int safeAngle = 5;
        private readonly int maxAngle = 45;
        private readonly float safeZoneDampner = 0.1f;

        public override void Init()
        {
            base.Init();
            InitSpiritLevelEnities();
        }

        protected virtual void InitSpiritLevelEnities()
        {
            var level = Core.Scene.Content.Load<Texture2D>("level");
            levelEntity = CreateEntity("level");
            levelEntity.AddComponent(new SpriteRenderer(level));
            levelEntity.Transform.Position = new Vector2(1000, 400);
            levelEntity.Transform.SetScale(0.15f);

            var bubble = Core.Scene.Content.Load<Texture2D>("bubble");
            bubbleEntity = CreateEntity("bubble");
            bubbleEntity.AddComponent(new SpriteRenderer(bubble));
            bubbleEntity.Transform.Position = new Vector2(985, 400);
            bubbleEntity.Transform.SetScale(0.15f);
        }

        public override void Update()
        {
            base.Update();

            if (isAligned)
            {
                Finalise();
                return;
            }

            CheckForInput();
            ResolveAngle();
            UpdateSprites();

            //Check if aligned
            if (Math.Abs(angle) < 1 && speed.Equals(0.0f))
                isAligned = true;
        }

        protected virtual void UpdateSprites()
        {
            bubbleEntity.Transform.SetPosition(new Vector2(985 + angle * 1.5f, 400));
        }

        protected void ResolveAngle()
        {
            angle += speed;

            //If in the safe zone
            if (Math.Abs(angle) < safeAngle)
            {
                //Apply damnpner positive
                if (speed >= safeZoneDampner)
                    speed -= safeZoneDampner;
                //Apply dampner negative
                else if (speed <= -safeZoneDampner)
                    speed += safeZoneDampner;
                //In safe zone
                else if (speed < safeZoneDampner && speed > -safeZoneDampner)
                    speed = 0;

            }
            //Not in safe zone
            else
            {
                //degrade speed
                speed += angle / 100.0f;
            }

            if (angle > maxAngle)
            {
                speed = 0;
                angle = maxAngle;
            }
            else if (angle < -maxAngle)
            {
                speed = 0;
                angle = -maxAngle;
            }
        }

        protected void CheckForInput()
        {
            if (Input.IsKeyPressed(Keys.Left))
            {
                speed -= 3.0f;
            }
            if (Input.IsKeyPressed(Keys.Right))
            {
                speed += 3.0f;
            }
            if (Input.IsKeyPressed(Keys.A))
            {
                speed -= 1.5f;
            }
            if (Input.IsKeyPressed(Keys.D))
            {
                speed += 1.5f;
            }
        }

        protected abstract void Finalise();
    }
}
