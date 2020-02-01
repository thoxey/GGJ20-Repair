using Frames;
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
                return new TitleFrame("title_died");
            }
            return frames.Dequeue();
        }

        private void LoadFrameSequence()
        {
            frames.Enqueue(new HeartSurgeryFrame());


            frames.Enqueue(new TitleFrame("title_repair"));
            frames.Enqueue(new TitleFrame("title_job_handiman"));
            frames.Enqueue(new TitleFrame("title_hangpainting"));
            frames.Enqueue(new PaintingBalanceFrame());
            frames.Enqueue(new TitleFrame("title_planedoor"));
            frames.Enqueue(new TitleFrame("title_plunge"));
            frames.Enqueue(new TitleFrame("title_job_mechanic"));
            frames.Enqueue(new TitleFrame("title_pumptyres"));
            frames.Enqueue(new TitleFrame("title_alignwheels"));
            frames.Enqueue(new WheelAlignmentFrame());
            frames.Enqueue(new TitleFrame("title_replacelights"));
            frames.Enqueue(new TitleFrame("job_it"));
            frames.Enqueue(new TitleFrame("title_replaceparts"));
            frames.Enqueue(new TitleFrame("title_cleardust"));
            frames.Enqueue(new TitleFrame("title_restart"));
            frames.Enqueue(new TurnItOnAndOffFrame());
            frames.Enqueue(new TitleFrame("title_job_doctor"));
            frames.Enqueue(new TitleFrame("title_heart"));
            frames.Enqueue(new HeartSurgeryFrame());
            frames.Enqueue(new TitleFrame("title_retire"));
            frames.Enqueue(new TitleFrame("title_died"));
        }
    }
}
