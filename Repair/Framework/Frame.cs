using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;

namespace Framework
{
    public abstract class Frame
    {
        public Action Finish;

        protected List<Entity> props;


        protected Entity CreateEntity(string name)
        {
            var ret = Core.Scene.CreateEntity(name);
            props.Add(ret);
            return ret;
        }

        public virtual void Init()
        {
            props = new List<Entity>();
        }

        public virtual void Update()
        {
        
        }


        public virtual void Draw()
        {

        }

        public virtual void OnFinish()
        {
            foreach(var entity in props)
            {
                entity.Destroy();
            }
            Finish?.Invoke();
        }
    }
}
