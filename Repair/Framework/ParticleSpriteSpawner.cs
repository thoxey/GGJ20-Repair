using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Verlet;

namespace Framework
{
    public class ParticleSpriteSpawner : Component, IUpdatable
    {
        private bool IsInited = false;

        private Func<bool> shouldSpawn { get; set; }

        private float speedMultiplier;

        private VerletWorld world;

        private string spriteName;

        List<(Entity, Composite, long)> particles = new List<(Entity, Composite, long)>();

        public void InitParticleSystem(Func<bool> shouldSpawnGetter, string spriteName, float speedMultiplier)
        {
            shouldSpawn = shouldSpawnGetter;
            this.spriteName = spriteName;
            this.speedMultiplier = speedMultiplier;

            world = new VerletWorld(new Rectangle(0, 0, Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height));
            world.Gravity = new Vector2(0, 2000);
        }

        private Entity CreateSpriteEntity()
        {
            var particleSprite = Core.Scene.Content.Load<Texture2D>(spriteName);
            var particleEntity = Core.Scene.CreateEntity("particle" + Guid.NewGuid().ToString().Substring(0, 3));
            var sprite = new SpriteRenderer(particleSprite);
            sprite.SetLayerDepth(0);
            particleEntity.AddComponent(sprite);
            particleEntity.Transform.Position = new Vector2(0, 0);
            particleEntity.Transform.SetScale(0.2f);
            return particleEntity;
        }

        public void Update()
        {
            if (IsInited)
                return;
            SpawnParticles();
            ResolveParticles();
        }

        private void SpawnParticles()
        {
            if (shouldSpawn())
            {
                var composite = new Composite();
                var particle = new Particle(Input.MousePosition);
                particle.Mass = 100f;
                composite.AddParticle(particle);
                particle.ApplyForce(RandomUpwardsForce());
                world.AddComposite(composite);
                var timeOut = DateTime.Now.AddSeconds(1).Ticks;

                var particleSprite = CreateSpriteEntity();
                particles.Add((particleSprite, composite, timeOut));
            }
        }

        private void ResolveParticles()
        {
            world.Update();

            var particlesToClear = new List<(Entity, Composite, long)>();
            foreach (var particle in particles)
            {
                if(DateTime.Now.Ticks > particle.Item3)
                {
                    particle.Item1.Destroy();
                    world.RemoveComposite(particle.Item2);
                    particlesToClear.Add(particle);
                    
                }
                else
                {
                    particle.Item1.Transform.Position = particle.Item2.Particles[0].Position;
                }
            }
            foreach(var particleToRemove in particlesToClear)
            {
                particles.Remove(particleToRemove);
            }
        }

        private static System.Random rng = new System.Random();

        private Vector2 RandomUpwardsForce()
        {
            float seed = (float)rng.NextDouble() + 1.0f;
            float signSeed = (float)rng.NextDouble();
            var sign = signSeed > 0.5f ? -1f : 1f;

            return new Vector2(seed * speedMultiplier * sign, seed * speedMultiplier * -1);
        }
    }
}
