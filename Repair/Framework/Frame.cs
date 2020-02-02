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

        public virtual string HintSpriteName => "";

        protected List<Entity> props;

        protected long hintTime = long.MaxValue;

        private Entity hint;

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
            backgroundEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.99f);

            if(HintSpriteName != "")
            {
                var hintTexture = Core.Scene.Content.Load<Texture2D>(HintSpriteName);
                hint = CreateEntity("hint");
                hint.AddComponent(new SpriteRenderer(hintTexture));
                hint.Transform.Position = new Vector2(1100, -200);
                hint.Transform.SetScale(0.1f);
                hintTime = DateTime.Now.AddSeconds(5).Ticks;
            }
        }

        public virtual void Update()
        {
            if(DateTime.Now.Ticks > hintTime)
            {
                SpawnHint();
            }
        }

        bool hintSpawned;
        private void SpawnHint()
        {
            if(!hintSpawned)
            {
                hint.Transform.Position += new Vector2(0, 10);
                if (hint.Transform.Position.Y > 100)
                    hintSpawned = true;
            }
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
