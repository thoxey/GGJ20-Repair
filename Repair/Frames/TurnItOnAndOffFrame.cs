using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Frames
{
    public class TurnItOnAndOffFrame : Frame
    {
        private Entity computerEntity, buttonEntity, loadingIconEntity, errorEntity, profileEntity;

        private int loadingTicks;
        enum ComputerState
        {
            ERROR,
            LOADING,
            PROFILE,
            OFF
        }
        ComputerState state;

        public override void Init()
        {
            base.Init();

            var scene = Core.Scene;
            var computer = scene.Content.Load<Texture2D>("computer");
            computerEntity = CreateEntity("computer");
            computerEntity.AddComponent(new SpriteRenderer(computer));
            computerEntity.Transform.Position = new Vector2(400, 400);
            computerEntity.Transform.SetScale(0.2f);


            var button = scene.Content.Load<Texture2D>("power");
            buttonEntity = CreateEntity("button");
            buttonEntity.AddComponent(new SpriteRenderer(button));
            buttonEntity.Transform.Position = new Vector2(620, 270);
            buttonEntity.Transform.SetScale(0.12f);

            var loadingIcon = scene.Content.Load<Texture2D>("loading");
            loadingIconEntity = CreateEntity("loading");
            loadingIconEntity.AddComponent(new SpriteRenderer(loadingIcon));
            loadingIconEntity.Transform.Position = new Vector2(285, 320);
            loadingIconEntity.Transform.SetScale(0.12f);
            loadingIconEntity.SetEnabled(false);

            var error = scene.Content.Load<Texture2D>("error");
            errorEntity = CreateEntity("error");
            errorEntity.AddComponent(new SpriteRenderer(error));
            errorEntity.Transform.Position = new Vector2(285, 320);
            errorEntity.Transform.SetScale(0.15f);

            var profile = scene.Content.Load<Texture2D>("profile");
            profileEntity = CreateEntity("profile");
            profileEntity.AddComponent(new SpriteRenderer(profile));
            profileEntity.Transform.Position = new Vector2(285, 320);
            profileEntity.Transform.SetScale(0.12f);
            profileEntity.SetEnabled(false);
        }

        public override void Update()
        {
            Vector2 point = Input.CurrentMouseState.Position.ToVector2();
            if (Input.LeftMouseButtonPressed && (buttonEntity.Position - point).Length() < 100)
            {
                buttonEntity.Transform.SetScale(0.1f);
                switch(state)
                {
                    case ComputerState.ERROR:
                        errorEntity.SetEnabled(false);
                        state = ComputerState.OFF;
                        break;
                    case ComputerState.LOADING:
                        loadingIconEntity.SetEnabled(false);
                        state = ComputerState.OFF;
                        break;
                    case ComputerState.OFF:
                        loadingIconEntity.SetEnabled(true);
                        state = ComputerState.LOADING;
                        break;
                }
            }
            else if (Input.LeftMouseButtonReleased)
            {
                buttonEntity.Transform.SetScale(0.12f);
            }
            if(state == ComputerState.LOADING)
            {
                loadingIconEntity.Transform.SetRotationDegrees(loadingIconEntity.Transform.RotationDegrees + 4);
                loadingTicks++;
                if(loadingTicks > 80)
                {
                    state = ComputerState.PROFILE;
                    profileEntity.SetEnabled(true);
                    loadingIconEntity.SetEnabled(false);
                }
            } else if (state == ComputerState.PROFILE)
            {
                loadingTicks++;
                if(loadingTicks > 150)
                {
                    OnFinish();
                }
            }
        }
    }
}
