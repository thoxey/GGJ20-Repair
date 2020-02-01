using System;
using Nez;

namespace Framework
{
    public abstract class TimedFrame : Frame
    {
        protected float frameTime = 3.0f;

        private Action<ITimer> Timeout;

        protected TimedFrame() : base()
        {
            Core.Schedule(frameTime, false, this, Timeout);
            Timeout += OnTimeout;
        }

        protected abstract void OnTimeout(ITimer timer);
    }
}
