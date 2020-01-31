using System;
using Nez;

namespace Framework
{
    public class MasterScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            AddSceneComponent(new FrameManager());
        }
    }
}
