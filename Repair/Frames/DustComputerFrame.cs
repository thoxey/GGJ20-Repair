using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;
using Repair.Framework;
using Framework;
using Nez.Tweens;
using Random = System.Random;
using System.Linq;

namespace Repair.Frames
{
    public class DustComputerFrame : DrawFrame
    {

        int mAmountDust = 20;
        List<string> mDustNames = new List<string>();

        List<Entity> mDust = new List<Entity>();
        List<bool> mDustTweening = new List<bool>();
        Entity computerEntity, handEntity, computerLidEntity, sprayCanEntity;

        int deadDust = 0;

        Vector2 minComp = new Vector2(386, 278);
        Vector2 maxComp = new Vector2(746, 583);
        Vector2 compPos = new Vector2(515, 353);

        public DustComputerFrame()
        {

        }

        public override void OnNodeAdded(Vector2 NodePos)
        {
            for(int i = 0; i < mDust.Count; ++i)
            {
                if(!mDustTweening[i] && (mDust[i].Transform.Position - NodePos).Length() < 70.0f)
                {
                    Vector2 direction = mDust[i].Transform.Position - computerEntity.Transform.Position;
                    direction.Normalize();
                    direction *= 2000.0f;
                    mDust[i].Transform.TweenPositionTo(direction).Start();
                    mDustTweening[i] = true;
                }
            }
        }

        public override void Init()
        {
            base.Init();

            for (int i = 0; i < 5; ++i)
            {
                mDustNames.Add("air" + i);
            }

            var scene = Core.Scene;
            var computer = scene.Content.Load<Texture2D>("bigcomputer");
            computerEntity = CreateEntity("computer");
            computerEntity.AddComponent(new SpriteRenderer(computer));
            computerEntity.Transform.Position = compPos;
            computerEntity.Transform.SetScale(0.3f);
            computerEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.8f);

            SpawnDust();

            var computerLid = scene.Content.Load<Texture2D>("computerlid");
            computerLidEntity = CreateEntity("computerlid");
            computerLidEntity.AddComponent(new SpriteRenderer(computerLid));
            computerLidEntity.Transform.Position = compPos;
            computerLidEntity.Transform.SetScale(0.3f);
            computerLidEntity.Transform.TweenPositionTo(new Vector2(3000, 0), 4.0f).Start();
            computerLidEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.5f);

            var sprayCan = Core.Scene.Content.Load<Texture2D>("sprayCan");
            sprayCanEntity = CreateEntity("sprayCan");
            sprayCanEntity.AddComponent(new SpriteRenderer(sprayCan));
            sprayCanEntity.Transform.Position = new Vector2(400, 400);
            sprayCanEntity.Transform.SetScale(0.15f);
            sprayCanEntity.AddComponent(new FollowMouseComponent());
            sprayCanEntity.GetComponent<SpriteRenderer>().SetLayerDepth(0.0f);

            SetUpDrawArea(1.0f, minComp , maxComp);
        }

        public void SpawnDust()
        {
            Random rnd = new Random();
            for(int i = 0; i < mAmountDust; ++i)
            {
                var dust = Core.Scene.Content.Load<Texture2D>(mDustNames[i%5]);
                mDust.Add(CreateEntity("dust" + i));
                mDust[mDust.Count - 1].AddComponent(new SpriteRenderer(dust));
                float x = rnd.Next((int)minComp.X- 60, (int)maxComp.X - 60);
                float y = rnd.Next((int)minComp.Y - 60, (int)maxComp.Y - 60);
                mDust[mDust.Count - 1].Transform.Position = new Vector2(x, y);
                mDust[mDust.Count - 1].Transform.SetScale(0.1f);
                mDust[mDust.Count - 1].GetComponent<SpriteRenderer>().SetLayerDepth(0.6f);
                mDustTweening.Add(false);
            }
        }

        public override void Update()
        {
            base.Update();

            bool everythingTweening = true;
            foreach(bool tweening in mDustTweening)
            {
                if (!tweening)
                {
                    everythingTweening = false;
                }
            }
            if(everythingTweening)
            {
                OnFinish();
            }
        }
    }
}
