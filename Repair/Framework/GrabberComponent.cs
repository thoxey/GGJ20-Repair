using System;
using System.Collections.Generic;
using Nez;
namespace Repair.Framework
{
    public class GrabberComponent : Component, IUpdatable
    {
        List<Entity> mEntitiesHoveringOver = new List<Entity>();
        bool mGrabbing = false;
        public GrabberComponent()
        {

        }
        public void Update()
        {
            if(mEntitiesHoveringOver.Count > 0)
            {
                Entity.Transform.SetPosition(Input.MousePosition);
            } 
            else if (Entity.Enabled) 
            {
                Entity.SetEnabled(false);
            }

            for (int i = mEntitiesHoveringOver.Count - 1; i >= 0; i--)
            {
                var component = mEntitiesHoveringOver[i].GetComponent<GrabableComponent>();
                if (component != null && !component.IsInsideRadius(Transform.Position))
                {
                    mEntitiesHoveringOver.RemoveAt(i);
                }
            }
        }
        public void CheckEntityIsOverCursor(Entity entity) 
        {
            if(!mEntitiesHoveringOver.Contains(entity) && entity.GetComponent<GrabableComponent>() != null)
            {
                GrabableComponent component = entity.GetComponent<GrabableComponent>();
                if(component.IsInsideRadius(Input.MousePosition)) 
                {
                    mEntitiesHoveringOver.Add(entity);
                    Entity.SetEnabled(true);
                }
            }
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            Console.WriteLine("Enabled");
        }

        public override void OnDisabled()
        {
            base.OnEnabled();
            Console.WriteLine("Disabled");
        }

        public bool IsGrabbing()
        {
            return mGrabbing;
        }

        public void Grab()
        {
            mGrabbing = true;
        }

        public void LetGo()
        {
            mGrabbing = false;
        }
    }
}
