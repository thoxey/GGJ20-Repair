using System;
using Nez;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez.Sprites;

namespace Frames
{
    public class ScoreFrame : Frame
    {
        int xOffset = 100;

        Vector2 ScoreBoardPos;

        Entity digit0, digit1, digit2, digit3;
        string val0, val1, val2, val3;

        public override void Init()
        {
            base.Init();
            ScoreBoardPos = new Vector2(Core.GraphicsDevice.Viewport.Width/2, (Core.GraphicsDevice.Viewport.Height/4) * 3);
            CreateScoreBoard();
        }

        public void RegisterTime(int digit0, int digit1, int digit2, int digit3)
        {
            val0 = digit0.ToString();
            val1 = digit1.ToString();
            val2 = digit2.ToString();
            val3 = digit3.ToString();
        }

        private void CreateScoreBoard()
        {
            var digit0Texture = Core.Scene.Content.Load<Texture2D>(val0);
            digit0 = CreateEntity("digit0");
            digit0.AddComponent(new SpriteRenderer(digit0Texture));
            digit0.Transform.Position = ScoreBoardPos + new Vector2(-xOffset * 2, 0);
            digit0.Transform.SetScale(0.15f);

            var digit1Texture = Core.Scene.Content.Load<Texture2D>(val1);
            digit1 = CreateEntity("digit1");
            digit1.AddComponent(new SpriteRenderer(digit1Texture));
            digit1.Transform.Position = ScoreBoardPos + new Vector2(-xOffset, 0);
            digit1.Transform.SetScale(0.15f);

            var digit2Texture = Core.Scene.Content.Load<Texture2D>(val2);
            digit2 = CreateEntity("digit2");
            digit2.AddComponent(new SpriteRenderer(digit2Texture));
            digit2.Transform.Position = ScoreBoardPos + new Vector2(xOffset, 0);
            digit2.Transform.SetScale(0.15f);

            var digit3Texture = Core.Scene.Content.Load<Texture2D>(val3);
            digit3 = CreateEntity("digit3");
            digit3.AddComponent(new SpriteRenderer(digit3Texture));
            digit3.Transform.Position = ScoreBoardPos + new Vector2(xOffset * 2, 0);
            digit3.Transform.SetScale(0.15f);

            var topDotTexture = Core.Scene.Content.Load<Texture2D>("air2");
            var topDot = CreateEntity("topDot");
            topDot.AddComponent(new SpriteRenderer(topDotTexture));
            topDot.Transform.Position = ScoreBoardPos + new Vector2(0, -20);
            topDot.Transform.SetScale(0.15f);

            var botDotTexture = Core.Scene.Content.Load<Texture2D>("air3");
            var botDot = CreateEntity("topDot");
            botDot.AddComponent(new SpriteRenderer(botDotTexture));
            botDot.Transform.Position = ScoreBoardPos + new Vector2(0, 20);
            botDot.Transform.SetScale(0.15f);
        }
    }
}
