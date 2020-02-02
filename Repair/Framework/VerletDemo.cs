using Nez;
using Nez.Verlet;
using Microsoft.Xna.Framework;

public class VerletDemo : RenderableComponent, IUpdatable
{
    public override float Width { get { return 800; } }
    public override float Height { get { return 600; } }

    VerletWorld _world;

    public override void OnAddedToEntity()
    {
        // create the verlet world which handles simulation
        _world = new VerletWorld(new Rectangle(0, 0, 800, 600));

        // create the Composite object
        var composite = new Composite();

        // add some particles. In this case we have three particles in a 'v' shape
        composite.AddParticle(new Particle(new Vector2(50, 50)));
        composite.AddParticle(new Particle(new Vector2(150, 50)));
        composite.AddParticle(new Particle(new Vector2(150, 150)));

        // add a squishy AngleConstraint to keep the 'v' angle in place
        composite.AddConstraint(new AngleConstraint(composite.Particles[0], composite.Particles[1], composite.Particles[2], 0.1f));

        // add two DistanceConstraints so the two ends of the 'v' keep their length fairly constant (0.8 stiffness so there is a little give)
        composite.AddConstraint(new DistanceConstraint(composite.Particles[0], composite.Particles[1], 0.8f));
        composite.AddConstraint(new DistanceConstraint(composite.Particles[1], composite.Particles[2], 0.8f));

        // add the Composite to the World simulation
        _world.AddComposite(composite);
    }

    public void Update()
    {
        _world.Update();
    }

    public override void Render(Batcher batcher, Camera camera)
    {
        _world.DebugRender(batcher);
    }
}