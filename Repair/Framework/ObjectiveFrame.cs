using System;
using Framework;

namespace Framework
{
    public abstract class ObjectiveFrame : Frame
    {
        public Action ObjectiveComplete;

        protected override void Init()
        {
            base.Init();
        }

        protected void OnObjectiveComplete()
        {
            ObjectiveComplete.Invoke();
        }
    }
}
