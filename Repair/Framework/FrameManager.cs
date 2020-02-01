using System;
using Nez;
using Repair.Frames;
using System.Collections.Generic;

namespace Framework
{
    public class FrameManager : SceneComponent
    {
        public int CurrentFrame = 0;

        public List<Frame> frames = new List<Frame>();

        public override void OnEnabled()
        {
            base.OnEnabled();

            frames.Add(new TurnItOnAndOffFrame());
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space) || (CurrentFrame < frames.Count && frames[CurrentFrame].IsFinished()))
                CurrentFrame++;
            else if (CurrentFrame < frames.Count)
                frames[CurrentFrame].Update();
        }
    }
}
