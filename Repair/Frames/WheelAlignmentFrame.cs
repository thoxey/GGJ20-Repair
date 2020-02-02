using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Frames
{
    public class WheelAlignmentFrame : BalanceFrame
    {
        Entity wheel, aligner, car;

        float finaliseScale = 0.2f;

        protected override void InitSpiritLevelEnities()
        {
            base.InitSpiritLevelEnities();

            var pos = new Vector2(640, 400);

            var carTexture = Core.Scene.Content.Load<Texture2D>("carforalign");
            car = CreateEntity("car");
            car.AddComponent(new SpriteRenderer(carTexture));
            car.Transform.Position = pos + new Vector2(80, -90);
            car.Transform.SetScale(0.17f);

            var alignerTexture = Core.Scene.Content.Load<Texture2D>("aligner");
            aligner = CreateEntity("aligner");
            aligner.AddComponent(new SpriteRenderer(alignerTexture));
            aligner.Transform.Position = pos + new Vector2(-140, 50);
            aligner.Transform.SetScale(0.17f);

            var wheelTexture = Core.Scene.Content.Load<Texture2D>("wheelside");
            wheel = CreateEntity("wheel");
            wheel.AddComponent(new SpriteRenderer(wheelTexture));
            wheel.Transform.Position = pos;
            wheel.Transform.SetScale(0.17f);
        }

        protected override void UpdateSprites()
        {
            base.UpdateSprites();
            wheel.Transform.SetRotationDegrees(angle);
        }

        float timer = 0f;

        protected override void Finalise()
        {
            wheel.Transform.SetRotationDegrees(0);
            if (timer < 1.0f)
            {
                timer += 0.03f;
                return;
            }

            if (finaliseScale < 3.0f)
            {
                finaliseScale += 0.03f;
                wheel.Transform.Scale = new Vector2(finaliseScale);
            }
            else if (finaliseScale >= 3.0f)
            {
                OnFinish();
            }
        }
    }
}
