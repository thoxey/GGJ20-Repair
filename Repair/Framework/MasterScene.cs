using System;
using Nez;

namespace Framework
{
    public class MasterScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void OnStart()
        {
            base.OnStart();
            AddSceneComponent(new FrameManager());
        }
    }
}
