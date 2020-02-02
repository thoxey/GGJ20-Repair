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

        private List<string> spriteNames;

        private float spriteDepth;

        private int numberOfSpritesSpawned = 0;

        private float degreesRotateSprite, scaleSprites;

        private Vector2 startVelocity, jitter;


        List<(Entity, Composite, long)> particles = new List<(Entity, Composite, long)>();

        public void InitParticleSystem(Func<bool> shouldSpawnGetter, List<string> spriteNames, float speedMultiplier, Vector2 startVelocity, Vector2 jitter, float scaleSprites = 1.0f, float degreesRotateSprite = 0.0f, float depth = 0.0f)
        {
            shouldSpawn = shouldSpawnGetter;
            this.spriteNames = spriteNames;
            this.speedMultiplier = speedMultiplier;
            this.degreesRotateSprite = degreesRotateSprite;
            this.jitter = jitter;
            this.startVelocity = startVelocity;
            this.scaleSprites = scaleSprites;

            spriteDepth = depth;

            world = new VerletWorld(new Rectangle(0, 0, Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height));
            world.Gravity = new Vector2(0, 2000);
        }

        private Entity CreateSpriteEntity()
        {
            numberOfSpritesSpawned++;
            var particleSprite = Core.Scene.Content.Load<Texture2D>(spriteNames[numberOfSpritesSpawned % spriteNames.Count]);
            var particleEntity = Core.Scene.CreateEntity("particle" + Guid.NewGuid().ToString().Substring(0, 3));
            var sprite = new SpriteRenderer(particleSprite);
            sprite.SetLayerDepth(spriteDepth);
            particleEntity.AddComponent(sprite);
            particleEntity.Transform.Position = new Vector2(0, 0);
            particleEntity.Transform.SetScale(scaleSprites);
            return particleEntity;
        }

        public override void OnRemovedFromEntity()
        {
            base.OnRemovedFromEntity();
            var particlesToClear = new List<(Entity, Composite, long)>();
            foreach (var particle in particles)
            {
                particle.Item1.Destroy();
                world.RemoveComposite(particle.Item2);
                particlesToClear.Add(particle);
            }
            particles.Clear();
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
                var particle = new Particle(Entity.Transform.Position);
                particle.Mass = 100f;
                composite.AddParticle(particle);
                particle.ApplyForce(JitteredForce());
                world.AddComposite(composite);
                var timeOut = DateTime.Now.AddSeconds(5).Ticks;

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
                if(DateTime.Now.Ticks > particle.Item3 || particle.Item1.Transform.Position.Y > 700)
                {
                    particle.Item1.Destroy();
                    world.RemoveComposite(particle.Item2);
                    particlesToClear.Add(particle);

                }

                else
                {
                    particle.Item1.Transform.Position = particle.Item2.Particles[0].Position;
                    Vector2 velocity = particle.Item2.Particles[0].Position - particle.Item2.Particles[0].LastPosition;


                    double theta1 = Math.Atan2(- 1,0) * (180 / Math.PI);

                    double theta2 = Math.Atan2(0 - velocity.Y, 0 - velocity.X) * (180 / Math.PI);

                    double diff = Math.Abs(theta1 - theta2);
                    double angle = Math.Min(diff, Math.Abs(180 - diff));

                    velocity.Normalize();
                    Vector2 tmp = new Vector2(0, -1);
                    float dot = velocity.X* tmp.X+ velocity.Y * tmp.Y;

                    float angleToAdd = (float)Math.Acos(dot) * (float)(180 / Math.PI);
                    if (velocity.X < 0) angleToAdd *= -1;

                    particle.Item1.Transform.SetRotationDegrees(degreesRotateSprite + angleToAdd);
                }
            }
            foreach(var particleToRemove in particlesToClear)
            {
                particles.Remove(particleToRemove);
            }
        }




        private static System.Random rng = new System.Random();

        private Vector2 JitteredForce()
        {
            return new Vector2(startVelocity.X + (float)rng.NextDouble() * 2 * jitter.X - jitter.X,
                startVelocity.Y + (float)rng.NextDouble() * 2 * jitter.Y - jitter.Y);
        }
    }
}
