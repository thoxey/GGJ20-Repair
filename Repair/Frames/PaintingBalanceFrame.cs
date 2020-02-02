using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Frames
{
    public class PaintingBalanceFrame : BalanceFrame
    {
        Entity paintingEntity;

        float paintingScale = 0.18f;

        protected override void InitSpiritLevelEnities()
        {
            base.InitSpiritLevelEnities();

            var squid = Core.Scene.Content.Load<Texture2D>("squid");
            paintingEntity = CreateEntity("painting");
            paintingEntity.AddComponent(new SpriteRenderer(squid));
            paintingEntity.Transform.Position = new Vector2(500, 350);
            paintingEntity.Transform.SetScale(paintingScale);
        }

        protected override void UpdateSprites()
        {
            base.UpdateSprites();
            paintingEntity.Transform.SetRotationDegrees(angle);
        }

        float timer = 0f;

        protected override void Finalise()
        {
            paintingEntity.Transform.SetRotationDegrees(0);
            if (timer < 1.0f)
            {
                timer += 0.03f;
                return;
            }

            if (paintingScale < 1.0f)
            {
                paintingScale += 0.03f;
                paintingEntity.Transform.Scale = new Vector2(paintingScale);
            }
            else if (paintingScale >= 1.0f)
            {
                OnFinish();
            }
        }
    }
}
