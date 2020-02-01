using Nez;

namespace Framework
{
    public class FrameManager : SceneComponent
    {
        public Frame CurrentFrame;

        private FrameSequencer frameSequencer;

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
            CurrentFrame.Init();
            CurrentFrame.Finish += LoadNextFrame;
        }
    }
}
