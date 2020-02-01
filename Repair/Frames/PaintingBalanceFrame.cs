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

        float paintingScale = 0.2f;

        protected override void InitSpiritLevelEnities()
        {
            base.InitSpiritLevelEnities();

            var squid = Core.Scene.Content.Load<Texture2D>("squid");
            paintingEntity = CreateEntity("painting");
            paintingEntity.AddComponent(new SpriteRenderer(squid));
            paintingEntity.Transform.Position = new Vector2(400, 400);
            paintingEntity.Transform.SetScale(paintingScale);
        }

        protected override void UpdateSprites()
        {
            base.UpdateSprites();
            paintingEntity.Transform.SetRotationDegrees(angle);
        }

        protected override void Finalise()
        {
            if (paintingScale < 3.0f)
            {
                paintingScale += 0.03f;
                paintingEntity.Transform.Scale = new Vector2(paintingScale);
            }
            else if (paintingScale >= 3.0f)
            {
                OnFinish();
            }
        }
    }
}
