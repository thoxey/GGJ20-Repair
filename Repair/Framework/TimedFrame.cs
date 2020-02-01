using System;
using Nez;

namespace Framework
{
    public abstract class TimedFrame : Frame
    {
        protected float frameTime = 3.0f;

        private Action<ITimer> Timeout;

        public override void Init()
        {
            base.Init();
            Timeout += OnTimeout;
            Core.Schedule(frameTime, false, Timeout);
        }

        protected void OnTimeout(ITimer timer) => OnFinish();
    }
}
