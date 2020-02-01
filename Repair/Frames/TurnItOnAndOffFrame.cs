using System;
using Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Repair.Frames
{
    public class TurnItOnAndOffFrame : Frame
    {
        private Entity computerEntity, buttonEntity, loadingIconEntity, errorEntity, profileEntity;
        public TurnItOnAndOffFrame()
        {
            var scene = Core.Scene;
            var computer = scene.Content.Load<Texture2D>("../assets/computer.png");
            computerEntity = scene.CreateEntity("computer");
            computerEntity.AddComponent(new SpriteRenderer(computer));
            computerEntity.Transform.Position = new Vector2(400, 400);

            var button = scene.Content.Load<Texture2D>("../assets/power.png");
            buttonEntity = scene.CreateEntity("button");
            buttonEntity.AddComponent(new SpriteRenderer(button));
            buttonEntity.Transform.Position = new Vector2(400, 400);

            var loadingIcon = scene.Content.Load<Texture2D>("../assets/loading.png");
            loadingIconEntity = scene.CreateEntity("computer");
            loadingIconEntity.AddComponent(new SpriteRenderer(loadingIcon));
            loadingIconEntity.Transform.Position = new Vector2(400, 400);

            var error = scene.Content.Load<Texture2D>("../assets/error.png");
            errorEntity = scene.CreateEntity("error");
            errorEntity.AddComponent(new SpriteRenderer(error));
            errorEntity.Transform.Position = new Vector2(400, 400);

            var profile = scene.Content.Load<Texture2D>("../assets/profile.png");
            profileEntity = scene.CreateEntity("computer");
            profileEntity.AddComponent(new SpriteRenderer(profile));
            profileEntity.Transform.Position = new Vector2(400, 400);
        }
    }
}
