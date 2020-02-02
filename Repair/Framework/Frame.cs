using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

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
            var background = Core.Scene.Content.Load<Texture2D>("background");
            var backgroundEntity = CreateEntity("background");
            backgroundEntity.AddComponent(new SpriteRenderer(background));
            backgroundEntity.Transform.Position = new Vector2(650, 400);
            backgroundEntity.Transform.SetScale(0.4f);
        }

        public virtual void Update()
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
