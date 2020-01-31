using System;
using Nez;

namespace Framework
{
    public class FrameManager : SceneComponent
    {
        public Frame CurrentFrame;

        public FrameManager()
        {
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                PrepareNextFrame();
        }

        public void PrepareNextFrame()
        {
            CurrentFrame = new Frame();
        }
    }
}
