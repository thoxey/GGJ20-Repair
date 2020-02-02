using System;
using System.Diagnostics;
using Frames;
using Nez;

namespace Framework
{
    public class FrameManager : SceneComponent
    {
        public Frame CurrentFrame;

        private FrameSequencer frameSequencer;

        Stopwatch stopWatch;

        private float score;

        public FrameManager()
        {
            stopWatch = new Stopwatch();
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            frameSequencer = new FrameSequencer();
            LoadNextFrame();
        }

        public override void Update()
        {
            CurrentFrame?.Update();
        }

        private void LoadNextFrame()
        {
            CurrentFrame = frameSequencer.GetNextFrame();
            if(CurrentFrame is ScoreFrame score)
            {
                TimeSpan ts = stopWatch.Elapsed;
                score.SetTime(ts.Seconds);
            }
            else if(!(CurrentFrame is TitleFrame))
            {
                stopWatch.Start();
            }
            else
            {
                stopWatch.Stop();
            }

            CurrentFrame.Init();
            CurrentFrame.Finish += LoadNextFrame;
        }
    }
}
