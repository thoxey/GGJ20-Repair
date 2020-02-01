using System;
using Nez;

namespace Framework
{
    public class FrameManager : SceneComponent
    {
        public Frame CurrentFrame;

        public override void OnEnabled()
        {
            base.OnEnabled();

            CurrentFrame = new TitleFrame("Pizza Wizard");
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                PrepareNextFrame();
            else
                CurrentFrame?.Update();
        }

        public void PrepareNextFrame()
        {
            //CurrentFrame.Finish();
        }
    }
}
