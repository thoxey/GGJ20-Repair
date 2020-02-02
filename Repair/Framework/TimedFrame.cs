using System;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace Framework
{
    public abstract class TimedFrame : Frame
    {
        protected float frameTime = 1.0f;

        protected Action<ITimer> Timeout;

        public override void Init()
        {
            base.Init();
            Timeout += OnTimeout;
            Core.Schedule(frameTime, false, Timeout);
        }

        protected void OnTimeout(ITimer timer) => OnFinish();
    }
}
