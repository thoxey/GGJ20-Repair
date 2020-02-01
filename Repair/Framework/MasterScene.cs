using System;
using Nez;
using Repair.Framework;

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
            Entity FollowMouse = CreateEntity("mouse");
            FollowMouse.AddComponent<FollowMouseComponent>();
        }
    }
}
