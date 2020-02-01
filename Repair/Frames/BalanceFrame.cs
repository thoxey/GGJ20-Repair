using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Repair.Frames
{
    public class BalanceFrame : Frame
    {
        float balancer = 45;
        float speed = 0;
        float paintingScale = 0.2f;

        Entity paintingEntity, levelEntity, bubbleEntity;

        public override void Init()
        {
            base.Init();

            var scene = Core.Scene;
            var level = scene.Content.Load<Texture2D>("level");
            levelEntity = CreateEntity("level");
            levelEntity.AddComponent(new SpriteRenderer(level));
            levelEntity.Transform.Position = new Vector2(1000, 400);
            levelEntity.Transform.SetScale(0.15f);

            var bubble = scene.Content.Load<Texture2D>("bubble");
            bubbleEntity = CreateEntity("bubble");
            bubbleEntity.AddComponent(new SpriteRenderer(bubble));
            bubbleEntity.Transform.Position = new Vector2(985, 400);
            bubbleEntity.Transform.SetScale(0.15f);

            var squid = scene.Content.Load<Texture2D>("squid");
            paintingEntity = CreateEntity("painting");
            paintingEntity.AddComponent(new SpriteRenderer(squid));
            paintingEntity.Transform.Position = new Vector2(400, 400);
            paintingEntity.Transform.SetScale(paintingScale);
        }

        public override void Update()
        {
            base.Update();
            if (Input.IsKeyPressed(Keys.Left)) {
                speed -= 5.0f;
            }
            if (Input.IsKeyPressed(Keys.Right))
            {
                speed += 5.0f;
            }
            if (Input.IsKeyPressed(Keys.A)) {
                speed -= 2.5f;
            }
            if (Input.IsKeyPressed(Keys.D)) {
                speed += 2.5f;
            }
            if (Input.IsKeyPressed(Keys.Q))
            {
                speed -= 0.5f;
            }
            if (Input.IsKeyPressed(Keys.E))
            {
                speed += 0.5f;
            }

            balancer += speed;

            float p = 0.1f;

            if (Math.Abs(balancer) < 5)
            {
                if (speed >= p) speed -= p;
                else if (speed < p && speed > -p) speed = 0;
                else if (speed <= -p) speed += p;
            }
            else
            {
                speed += balancer / 100.0f;
            }

            //if (balancer > 2) speed += 1 - (float)Math.Pow(2, balancer);
            //else if (balancer < -2) speed -= 1 - (float)Math.Pow(2, balancer);

            if (balancer > 45) { speed = 0;  balancer = 45; }

            else if (balancer < -45) { speed = 0;  balancer = -45; }

            paintingEntity.Transform.SetRotationDegrees(balancer);
            bubbleEntity.Transform.SetPosition(new Vector2(985 + balancer * 1.5f, 400));

            if(Math.Abs(balancer) < 1 && speed.Equals(0.0f) && paintingScale < 3.0f)
            {
                paintingScale += 0.01f;
                paintingEntity.Transform.SetScale(paintingScale);
            } else if (paintingScale >= 3.0f)
            {
                OnFinish();
            }
        }
    }
}
