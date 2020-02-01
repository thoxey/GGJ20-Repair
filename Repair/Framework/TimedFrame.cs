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

            Timeout += OnTimeout;
            Core.Schedule(frameTime, false, Timeout);
        }

        protected abstract void OnTimeout(ITimer timer);
    }
}
