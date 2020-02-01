using System;
using Framework;

namespace Framework
{
    public abstract class ObjectiveFrame : Frame
    {
        private Action ObjectiveComplete;

        protected ObjectiveFrame() : base()
        {
            ObjectiveComplete += OnObjectiveComplete;
        }

        protected abstract void OnObjectiveComplete();
    }
}
