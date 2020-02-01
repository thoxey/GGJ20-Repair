using System;
using Nez;
namespace Repair.Framework
{
    public class FollowMouseComponent : Component, IUpdatable
    {
        public FollowMouseComponent()
        {

        }
        public void Update() {
            Transform.SetPosition(Input.MousePosition);
          }

    }
}
