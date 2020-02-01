using Repair.Frames;
using System.Collections.Generic;

namespace Framework
{

    internal class FrameSequencer
    {
        private Queue<Frame> frames = new Queue<Frame>();

        private bool init = true;

        internal FrameSequencer()
        {
        }

        public Frame GetNextFrame()
        {
            if (init)
            {
                init = false;
                LoadFrameSequence();
            }
            else if (frames.Count == 0)
            {
                return new TitleFrame("error");
            }
            return frames.Dequeue();
        }

        private void LoadFrameSequence()
        {
            //frames.Enqueue(new TitleFrame("Pizza Wizard"));
            frames.Enqueue(new PaintingBalanceFrame());
            //frames.Enqueue(new TurnItOnAndOffFrame());
            //frames.Enqueue(new TitleFrame("Pizza Wizard"));
        }
    }
}