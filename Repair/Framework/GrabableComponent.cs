using System;
using Microsoft.Xna.Framework;
using Nez;
namespace Repair.Framework
{
    public class GrabableComponent : Component, IUpdatable
    {
        private int grabid = 0;
        private float mRadius = 0.0f;
        private bool mBeingGrabbed = false;
        private Entity mCursor;
        public GrabableComponent(float radius, Entity cursor)
        {
            mRadius = radius;
            if(cursor.GetComponent<GrabberComponent>() != null) 
            {
                mCursor = cursor;
            }
            Console.WriteLine("started " + grabid++);
        }
        public void Update()
        {
                mCursor.GetComponent<GrabberComponent>().CheckEntityIsOverCursor(Entity);
                bool isMousePositionInsideRadius = IsInsideRadius(Input.MousePosition);

                if (isMousePositionInsideRadius)
                {
                    GrabberComponent component = mCursor.GetComponent<GrabberComponent>();
                    lock (component)
                    {
                        if (Input.LeftMouseButtonPressed && !component.IsGrabbing())
                        {
                            mBeingGrabbed = true;
                            component.Grab();

                        }
                    }
                }

                if (mBeingGrabbed && Input.LeftMouseButtonDown)
                {
                    Entity.Transform.SetPosition(Input.MousePosition);
                }
                else if (mBeingGrabbed && !Input.LeftMouseButtonDown)
                {
                    mBeingGrabbed = false;
                    mCursor.GetComponent<GrabberComponent>().LetGo();
                }
            
        }
        public bool IsInsideRadius(Vector2 position) 
        {
            return (position - Entity.Position).Length() < mRadius;
        }
    }
}
